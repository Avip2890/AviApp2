using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.OrderQueries;

public record GetOrderByIdQuery(int Id) : IRequest<Result<OrderDto>>;