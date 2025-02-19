using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;
namespace AviApp.Api.Orders.GetAllOrders;

public class GetAllOrdersHandler(IOrderService orderService)
    : IRequestHandler<GetAllOrdersQuery, Result<List<OrderDto>>>
{
    public async Task<Result<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await orderService.GetAllOrdersAsync(cancellationToken);

        return result.IsSuccess
            ? result.Value.Select(order => order.ToDto()).ToList()
            : result.Errors;
    }
}