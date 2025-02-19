using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<Result<OrderDto>>;