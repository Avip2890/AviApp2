using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.OrderQueries;
using MediatR;

namespace AviApp.Handlers.OrderHandlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly IOrderService _orderService;

    public GetOrderByIdHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderService.GetOrderById(request.Id));
    }
}