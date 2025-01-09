using AviApp.Api.Order.OrderQueries;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.OrderHandlers;

public class GetOrderByIdHandler(IOrderService orderService) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await orderService.GetOrderByIdAsync(request.Id, cancellationToken);
        
        if (!result.IsSuccess) return null;
    
        var order = result.Value;

        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            Items = order.Items.Select(item => item.Id).ToList()
        };
    }
}