using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.Order.UpdateOrder;

public class UpdateOrderCommandHandler(IOrderService orderService) : IRequestHandler<UpdateOrderCommand, OrderDto?>
{
    public async Task<OrderDto?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
       
        var existingOrderResult = await orderService.GetOrderByIdAsync(request.Id, cancellationToken);

        if (!existingOrderResult.IsSuccess || existingOrderResult.Value == null)
        {
            return null; 
        }

        var existingOrder = existingOrderResult.Value;

      
        existingOrder.OrderDate = request.OrderDate;
        existingOrder.CustomerId = request.CustomerId;
        
        var itemIds = request.Items.Select(item => item.Id);
        var menuItemsResult = await orderService.GetMenuItemsByIdsAsync(itemIds, cancellationToken);
        if (!menuItemsResult.IsSuccess || menuItemsResult.Value == null || !menuItemsResult.Value.Any())
        {
            throw new InvalidOperationException("Failed to fetch menu items for the order.");
        }
        existingOrder.Items = menuItemsResult.Value;
        
        var updatedOrderResult = await orderService.UpdateOrderAsync(request.Id, existingOrder, cancellationToken);

        if (!updatedOrderResult.IsSuccess || updatedOrderResult.Value == null)
        {
            return null; 
        }
        
        return updatedOrderResult.Value.ToDto();
    }
}