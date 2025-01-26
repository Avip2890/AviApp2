using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.DeleteOrder;

public class DeleteOrderHandler(IOrderService orderService) : IRequestHandler<DeleteOrderCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await orderService.DeleteOrderAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            return Error.BadRequest("DeleteOrder failed");
        }

        return new Deleted();
    }
}