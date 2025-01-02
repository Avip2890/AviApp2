using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.OrderQueries;
using MediatR;

namespace AviApp.Handlers.OrderHandlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderService _orderService;

    public GetAllOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderService.GetAllOrders());
    }
}