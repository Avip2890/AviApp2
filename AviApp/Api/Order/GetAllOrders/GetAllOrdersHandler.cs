
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.GetAllOrders;

public class GetAllOrdersHandler(IOrderService orderService)
    : IRequestHandler<GetAllOrdersQuery, Result<List<OrderDto>>>
{
    public async Task<Result<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await orderService.GetAllOrdersAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return Error.BadRequest("Couldn't get all orders'");
        }

        return Result<List<OrderDto>>.Success(result.Value.Select(order => order.ToDto()).ToList());
    }
}