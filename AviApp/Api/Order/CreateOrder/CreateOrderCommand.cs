using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto) : IRequest<Result<OrderDto>>;