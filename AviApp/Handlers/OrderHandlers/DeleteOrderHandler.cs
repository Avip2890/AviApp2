using AviApp.Commands.OrderCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.OrderHandlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderService _orderService;

    public DeleteOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderService.DeleteOrder(request.Id));
    }
}