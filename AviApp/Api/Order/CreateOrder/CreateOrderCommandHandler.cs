using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.CreateOrder;

public class CreateOrderHandler(IOrderService orderService) : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var menuItems = await orderService.GetMenuItemsByIdsAsync(request.OrderDto.Items, cancellationToken);
        if (!menuItems.IsSuccess)
        {
            return Result<OrderDto>.Failure(menuItems.Error);
        }

        var orderEntity = request.OrderDto.ToEntity(menuItems.Value);

        var result = await orderService.CreateOrderAsync(orderEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<OrderDto>.Failure(result.Error);
        }

        return Result<OrderDto>.Success(result.Value.ToDto());
    }
}