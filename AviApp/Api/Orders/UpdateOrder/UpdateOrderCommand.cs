using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.UpdateOrder;

public record UpdateOrderCommand(int Id, string CustomerName, string Phone, DateTime OrderDate, string Email, List<int> Items) 
    : IRequest<Result<OrderDto>>;