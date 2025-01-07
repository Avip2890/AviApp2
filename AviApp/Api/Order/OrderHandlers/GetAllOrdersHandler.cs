using AviApp.Api.Order.OrderQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.OrderHandlers;

public class GetAllOrdersHandler(IOrderService orderService) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        
        return await orderService.GetAllOrdersAsync(cancellationToken);
    }
}