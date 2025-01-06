using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;
using AviApp.Mappers;
using AviApp.Models;

namespace AviApp.Services;

public class OrderService : IOrderService
{
    private readonly AvipAppDbContext _context;

    public OrderService(AvipAppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<OrderDto> GetAllOrders()
    {
        return _context.Orders.Select(o => o.ToDto()).ToList();
    }

    public OrderDto? GetOrderById(int id)
    {
        var order = _context.Orders.Find(id);
        return order?.ToDto();
    }

    public OrderDto CreateOrder(OrderDto orderDto)
    {
        var orderEntity = orderDto.ToEntity();
        _context.Orders.Add(orderEntity);
        _context.SaveChanges();
        return orderEntity.ToDto();
    }

    public OrderDto? UpdateOrder(int id, OrderDto updatedOrderDto)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return null;
        }

        order.CustomerId = updatedOrderDto.CustomerId;
        order.OrderDate = updatedOrderDto.OrderDate;
        order.Items = updatedOrderDto.Items.Select(item => item.ToEntity()).ToList();

        _context.SaveChanges();
        return order.ToDto();
    }

    public bool DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        _context.SaveChanges();
        return true;
    }
}