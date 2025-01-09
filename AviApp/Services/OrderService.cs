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

        if (!orders.Any())
        {
            return Result<List<Order>>.Failure("No orders found.");
        }

        return Result<List<Order>>.Success(orders);
    }

    public async Task<Result<Order>> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order == null)
        {
            return Result<Order>.Failure($"Order with ID {id} not found.");
        }

        return Result<Order>.Success(order);
    }

    public async Task<Result<Order>> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            // Fetch related menu items from the database
            var menuItems = await context.MenuItems
                .Where(mi => order.Items.Select(i => i.Id).Contains(mi.Id))
                .ToListAsync(cancellationToken);

            if (!menuItems.Any())
            {
                return Result<Order>.Failure("No valid menu items found for the order.");
            }

            order.Items = menuItems;

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return Result<Order>.Success(order);
        }
        catch (Exception ex)
        {
            return Result<Order>.Failure($"Failed to create order: {ex.Message}");
        }
    }

    public async Task<Result<Order>> UpdateOrderAsync(int id, Order updatedOrder, CancellationToken cancellationToken = default)
    {
        var existingOrder = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (existingOrder == null)
        {
            return Result<Order>.Failure($"Order with ID {id} not found.");
        }

        existingOrder.CustomerId = updatedOrder.CustomerId;
        existingOrder.OrderDate = updatedOrder.OrderDate;

        // Update related menu items
        var menuItems = await context.MenuItems
            .Where(mi => updatedOrder.Items.Select(i => i.Id).Contains(mi.Id))
            .ToListAsync(cancellationToken);

        if (!menuItems.Any())
        {
            return Result<Order>.Failure("No valid menu items found for the updated order.");
        }

        existingOrder.Items = menuItems;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return Result<Order>.Success(existingOrder);
        }
        catch (Exception ex)
        {
            return Result<Order>.Failure($"Failed to update order: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order == null)
        {
            return Result<bool>.Failure($"Order with ID {id} not found.");
        }

        try
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Failed to delete order: {ex.Message}");
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
                return Result<List<MenuItem>>.Failure("No menu items found for the provided IDs.");
            }

            return Result<List<MenuItem>>.Success(menuItems);
        }
        catch (Exception ex)
        {
            return Result<List<MenuItem>>.Failure($"Failed to fetch menu items: {ex.Message}");
        }
    }
}
