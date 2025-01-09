using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.DeleteOrder;

public class DeleteOrderCommandHandler(IOrderService orderService) : IRequestHandler<DeleteOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await orderService.DeleteOrderAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Error);
        }
        return result.Value;
    }
}