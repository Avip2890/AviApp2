using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<Result<OrderDto>>;