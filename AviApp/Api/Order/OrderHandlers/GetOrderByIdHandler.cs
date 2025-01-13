using AviApp.Api.Order.OrderQueries;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.OrderHandlers;

public class GetOrderByIdHandler(IOrderService orderService) : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await orderService.GetOrderByIdAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<OrderDto>.Failure(result.Error);
        }

        return Result<OrderDto>.Success(result.Value.ToDto());
    }
}