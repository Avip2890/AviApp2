using AviApp.Interfaces;
using MediatR;

namespace AviApp.Api.Order.DeleteOrder;

public class DeleteOrderCommandHandler(IOrderService orderService) : IRequestHandler<DeleteOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        return await orderService.DeleteOrderAsync(request.Id, cancellationToken);
    }
}