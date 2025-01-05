
using AviApp.Domain.Entities;

namespace AviApp.Interfaces;

public interface IOrderService
{
    IEnumerable<Order> GetAllOrders();
    Order? GetOrderById(int id);
    Order CreateOrder(Order order);
    Order? UpdateOrder(int id, Order updatedOrder);
    bool DeleteOrder(int id);
}