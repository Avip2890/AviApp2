using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class OrderService(AvipAppDbContext context) : IOrderService
{
    public async Task<Result<List<Order>>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await context.Orders
            .Include(o => o.OrderMenuItems) 
            .ThenInclude(omi => omi.MenuItem)
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<Result<Order>> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders
            .Include(o => o.OrderMenuItems)
            .ThenInclude(omi => omi.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order == null)
        {
            return Error.NotFound("Order not found");
        }

        return order;
    }

    public async Task<Result<Order>> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        // בדיקה בטיחותית שהוזנו פריטים
        if (order.OrderMenuItems == null || !order.OrderMenuItems.Any())
        {
            return Error.BadRequest("חייבים לבחור לפחות פריט אחד בהזמנה.");
        }
        
        var menuItemIds = order.OrderMenuItems.Select(omi => omi.MenuItemId).ToList();

        var menuItems = await context.MenuItems
            .Where(mi => menuItemIds.Contains(mi.Id))
            .ToListAsync(cancellationToken);
        
        order.MenuItemName = string.Join(", ", menuItems.Select(mi => mi.Name));

        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);


        return order;
    }


    public async Task<Result<Order>> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default)
    {
        var existingOrder = await context.Orders
            .FirstOrDefaultAsync(o => o.Id == updatedOrder.Id, cancellationToken);

        if (existingOrder == null)
            return Error.NotFound($"Order with ID {updatedOrder.Id} not found.");

        existingOrder.Email = updatedOrder.Email;
        existingOrder.OrderDate = updatedOrder.OrderDate;
        existingOrder.CustomerName = updatedOrder.CustomerName;
        existingOrder.Phone = updatedOrder.Phone;

     
        var menuItemIds = updatedOrder.OrderMenuItems.Select(omi => omi.MenuItemId).ToList();

        var menuItems = await context.MenuItems
            .Where(mi => menuItemIds.Contains(mi.Id))
            .ToListAsync(cancellationToken);

        existingOrder.MenuItemName = string.Join(", ", menuItems.Select(mi => mi.Name));

        try
        {
            context.Entry(existingOrder).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return existingOrder;
        }
        catch
        {
            return Error.BadRequest("Error saving order");
        }
    }

    
    public async Task<Result<Deleted>> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order == null)
        {
            return Error.NotFound("Order not found");
        }

        try
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);
            return new Deleted();
        }
        catch
        {
            return Error.BadRequest("Failed to delete order");
        }
    }

    public async Task<Result<List<MenuItem>>> GetMenuItemsByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        try
        {
            var menuItems = await context.MenuItems
                .Where(mi => ids.Contains(mi.Id))
                .ToListAsync(cancellationToken);

            if (!menuItems.Any())
            {
                return Error.NotFound("MenuItem not found");
            }

            return menuItems;
        }
        catch
        {
            return Error.BadRequest("Failed to get menu items");
        }
    }
}
