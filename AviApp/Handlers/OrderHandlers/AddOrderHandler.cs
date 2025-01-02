using AviApp.Commands.OrderCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.OrderHandlers;

public class AddOrderHandler : IRequestHandler<AddOrderCommand, Models.Order>
{
    private readonly IOrderService _orderService;

    public AddOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<Models.Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = new Models.Order
        {
            CustomerId = request.CustomerId,
            Items = request.Items,
            OrderDate = request.OrderDate
        };

        return Task.FromResult(_orderService.CreateOrder(newOrder));
    }
}