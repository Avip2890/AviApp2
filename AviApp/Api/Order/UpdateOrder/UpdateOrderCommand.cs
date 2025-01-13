using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.UpdateOrder;

public record UpdateOrderCommand(int Id, OrderDto OrderDto) : IRequest<Result<OrderDto>>;
