using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.CreateOrder;

public class CreateOrderCommandHandler(IOrderService orderService) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var createdOrder = await orderService.CreateOrderAsync(request.OrderDto, cancellationToken);

        return createdOrder;
    }
}