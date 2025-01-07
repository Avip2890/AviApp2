using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.CreateOrder;

public abstract class CreateOrderCommand(OrderDto orderDto) : IRequest<OrderDto>
{
    public OrderDto OrderDto { get; } = orderDto;
}