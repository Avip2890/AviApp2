using AviApp.Api.Order.OrderQueries;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.OrderHandlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, Result<List<OrderDto>>>
{
    private readonly IOrderService _orderService;

    public GetAllOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<Result<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetAllOrdersAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<List<OrderDto>>.Failure(result.Error);
        }

        return Result<List<OrderDto>>.Success(result.Value.Select(order => order.ToDto()).ToList());
    }
}