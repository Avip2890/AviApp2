using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class OrderService(AvipAppDbContext context) : IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await context.Orders.Include(o => o.Items).ToListAsync(cancellationToken);
        return orders.Select(order => order.ToDto());
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        return order?.ToDto();
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default)
    {
        // שליפת הפריטים מה-DB לפי מזהים
        var menuItems = await context.MenuItems
            .Where(mi => orderDto.Items.Contains(mi.Id))
            .ToListAsync(cancellationToken);

       
        var orderEntity = new Order
        {
            OrderDate = orderDto.OrderDate,
            CustomerId = orderDto.CustomerId,
            Items = menuItems
        };

        context.Orders.Add(orderEntity);
        await context.SaveChangesAsync(cancellationToken);
        return orderEntity.ToDto();
    }

    public async Task<OrderDto?> UpdateOrderAsync(int id, OrderDto updatedOrderDto, CancellationToken cancellationToken = default)
    {
        var existingOrder = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (existingOrder == null) return null;

        existingOrder.CustomerId = updatedOrderDto.CustomerId;
        existingOrder.OrderDate = updatedOrderDto.OrderDate;

        // עדכון הפריטים הקשורים
        var menuItems = await context.MenuItems
            .Where(mi => updatedOrderDto.Items.Contains(mi.Id))
            .ToListAsync(cancellationToken);

        existingOrder.Items = menuItems;

        await context.SaveChangesAsync(cancellationToken);
        return existingOrder.ToDto();
    }

    public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (order == null) return false;

        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
