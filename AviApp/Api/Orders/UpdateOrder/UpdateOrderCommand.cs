using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.UpdateOrder;

public record UpdateOrderCommand(int Id, int CustomerId, DateTime OrderDate, List<int> Items) 
    : IRequest<Result<OrderDto>>;