
using AviApp.Models;

namespace AviApp.Interfaces;

public interface IOrderService
{
    IEnumerable<Order> GetAllOrders();
    Order? GetOrderById(int id);
    void CreateOrder(Order order);
    void UpdateOrder(int id, Order updatedOrder);
    void DeleteOrder(int id);
}