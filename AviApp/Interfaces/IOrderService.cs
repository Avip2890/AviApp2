using AviApp.Domain.Entities;
using AviApp.Results;

namespace AviApp.Interfaces
{
    public interface IOrderService
    {
        Task<Result<List<Order>>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<Result<Order>> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<Order>> CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Result<Order>> UpdateOrderAsync(int id, Order updatedOrder, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteOrderAsync(int id, CancellationToken cancellationToken);
        Task<Result<List<MenuItem>>> GetMenuItemsByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}