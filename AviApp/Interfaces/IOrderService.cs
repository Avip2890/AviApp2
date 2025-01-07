using AviApp.Models;

namespace AviApp.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
    Task<OrderDto?> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OrderDto> CreateOrderAsync(OrderDto order, CancellationToken cancellationToken = default);
    Task<OrderDto?> UpdateOrderAsync(int id, OrderDto updatedOrder, CancellationToken cancellationToken = default);
    Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default);
}