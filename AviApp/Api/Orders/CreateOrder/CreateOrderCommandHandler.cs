using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.CreateOrder;

public class CreateOrderHandler(IOrderService orderService) 
    : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var createOrderRequest = request.CreateOrderRequest;
        
        var menuItemsResult = await orderService.GetMenuItemsByIdsAsync(request.CreateOrderRequest.OrderMenuItems.Select(x => x.MenuItemId), cancellationToken);
    
        if (!menuItemsResult.IsSuccess || !menuItemsResult.Value.Any())
        {
            return Error.BadRequest("Couldn't find menu items.");
        }

        var orderEntity = new Order
        {
            CustomerId = createOrderRequest.CustomerId,
            OrderDate = createOrderRequest.OrderDate.Date
        };
        
        var result = await orderService.CreateOrderAsync(orderEntity, cancellationToken);
        
        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
}
