using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.UpdateOrder;

public class UpdateOrderHandler(IOrderService orderService) : IRequestHandler<UpdateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var menuItems = await orderService.GetMenuItemsByIdsAsync(request.OrderDto.Items, cancellationToken);
        if (!menuItems.IsSuccess)
            return Error.BadRequest("Order item not found");

        var updatedOrderEntity = request.OrderDto.ToEntity(menuItems.Value);
        updatedOrderEntity.Id = request.Id;

        var result = await orderService.UpdateOrderAsync(request.Id, updatedOrderEntity, cancellationToken);

        return result.IsSuccess
            ? result.Value.ToDto()
            : result.Errors;
    }
}