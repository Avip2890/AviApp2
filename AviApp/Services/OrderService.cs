using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public  class OrderService(AvipAppDbContext context) : IOrderService
{
    public async Task<Result<List<Order>>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await context.Orders.Include(o => o.Items).ToListAsync(cancellationToken);
        
        return orders;
    }

    public async Task<Result<Order>> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order == null)
        {
            return Error.NotFound("Order not found");
        }

        return order;
    }

    public async Task<Result<Order>> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            var menuItems = await context.MenuItems
                .Where(mi => order.Items.Select(i => i.Id).Contains(mi.Id))
                .ToListAsync(cancellationToken);

            if (!menuItems.Any())
            {
                return Error.NotFound("Order Not Found");
            }

            order.Items = menuItems;

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return order;
        }
        catch 
        {
            return Error.BadRequest("Error occurred while processing order");
        }
    }

    public async Task<Result<Order>> UpdateOrderAsync(int id, Order updatedOrder, CancellationToken cancellationToken = default)
    {
        var existingOrder = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (existingOrder == null)
        {
            return Error.NotFound("Order Not Found");
        }

        existingOrder.CustomerId = updatedOrder.CustomerId;
        existingOrder.OrderDate = updatedOrder.OrderDate;

        // Update related menu items
        var menuItems = await context.MenuItems
            .Where(mi => updatedOrder.Items.Select(i => i.Id).Contains(mi.Id))
            .ToListAsync(cancellationToken);

        if (!menuItems.Any())
        {
            return Error.NotFound("Order with ID {id} not found");
        }

        existingOrder.Items = menuItems;

        try
        {
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
            return Error.NotFound("Order Not Found");
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
                return Error.NotFound("MenuItem Not Found");
            }

            return menuItems;
        }
        catch
        {
            return Error.BadRequest("Failed to get menuItems");
        }
    }
}
