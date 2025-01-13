using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.DeleteOrder;

public class DeleteOrderHandler(IOrderService orderService) : IRequestHandler<DeleteOrderCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await orderService.DeleteOrderAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<bool>.Failure(result.Error);
        }

        return Result<bool>.Success(true);
    }
}