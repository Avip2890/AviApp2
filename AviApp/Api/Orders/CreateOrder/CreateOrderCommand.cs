using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.CreateOrder;

public record CreateOrderCommand(OrderDto CreateOrderRequest) 
    : IRequest<Result<OrderDto>>;

