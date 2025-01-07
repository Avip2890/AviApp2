using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.UpdateOrder;

public abstract class UpdateOrderCommand: IRequest<OrderDto?>
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public OrderDto Order { get; set; } = null!;
    public List<MenuItemDto> Items { get; set; } = new List<MenuItemDto>();
}