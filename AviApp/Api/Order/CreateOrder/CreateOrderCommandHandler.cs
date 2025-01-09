using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.CreateOrder;

public class CreateOrderCommandHandler(IOrderService orderService) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var menuItemsResult = await orderService.GetMenuItemsByIdsAsync(request.OrderDto.Items, cancellationToken);

        if (!menuItemsResult.IsSuccess)
        {
            throw new InvalidOperationException(menuItemsResult.Error);
        }
        
        var orderEntity = request.OrderDto.ToEntity(menuItemsResult.Value);
        var result = await orderService.CreateOrderAsync(orderEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Error);
        }
        
        return result.Value.ToDto();
    }
}