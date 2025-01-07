using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.UpdateOrder;

public class UpdateOrderCommandHandler(IOrderService orderService) : IRequestHandler<UpdateOrderCommand, OrderDto?>
{
    public async Task<OrderDto?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
       
        var updateOrderDto = new OrderDto
        {
            Id = request.Id,
            OrderDate = request.OrderDate,
            CustomerId = request.CustomerId,
            Items = request.Items.Select(item => item.Id).ToList() 
        };

        
        var updatedOrder = await orderService.UpdateOrderAsync(request.Id, updateOrderDto, cancellationToken);

        return updatedOrder;
    }
}