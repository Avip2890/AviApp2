using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;

namespace AviApp.Services;

public class OrderService : IOrderService
{
    private readonly AvipAppDbContext _context;

    public OrderService(AvipAppDbContext context)
    {
        _context = context;
    }


    public IEnumerable<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }

  
    public Order? GetOrderById(int id)
    {
        return _context.Orders.Find(id);
    }

   
    public Order CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

   
    public Order? UpdateOrder(int id, Order updatedOrder)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return null;
        }

        order.CustomerId = updatedOrder.CustomerId;
        order.OrderDate = updatedOrder.OrderDate;
        

        _context.SaveChanges();
        return order;
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