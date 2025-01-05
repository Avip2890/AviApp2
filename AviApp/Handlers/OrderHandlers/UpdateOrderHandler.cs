/*using AviApp.Commands.OrderCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.OrderHandlers;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Models.Order?>
{
    private readonly IOrderService _orderService;

    public UpdateOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public Task<Models.Order?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var updatedOrder = new Models.Order
        {
            CustomerId = request.CustomerId,
            Items = request.Items,
            OrderDate = request.OrderDate
        };

        return Task.FromResult(_orderService.UpdateOrder(request.Id, updatedOrder));
    }
    
}*/