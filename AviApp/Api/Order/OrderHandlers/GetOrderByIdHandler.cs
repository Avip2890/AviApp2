using AviApp.Api.Order.OrderQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.OrderHandlers;

public class GetOrderByIdHandler(IOrderService orderService) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
       
        return await orderService.GetOrderByIdAsync(request.Id, cancellationToken);
    }
}