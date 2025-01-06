using AviApp.Models;

namespace AviApp.Interfaces;

public interface IOrderService
{
    IEnumerable<OrderDto> GetAllOrders();
    OrderDto? GetOrderById(int id);
    OrderDto CreateOrder(OrderDto order);
    OrderDto? UpdateOrder(int id, OrderDto updatedOrder);
    bool DeleteOrder(int id);
}