using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.CreateOrder;

public record CreateOrderCommand(int CustomerId, List<int> Items, DateTime? OrderDate = null) 
    : IRequest<Result<OrderDto>>;

