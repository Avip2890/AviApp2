using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.UpdateOrder;

public class UpdateOrderHandler(IOrderService orderService)
    : IRequestHandler<UpdateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrderResult = await orderService.GetOrderByIdAsync(request.Id, cancellationToken);
        if (!existingOrderResult.IsSuccess)
        {
            return Error.NotFound($"Order with ID {request.Id} not found.");
        }

        var existingOrder = existingOrderResult.Value;

        existingOrder.Email = request.Email;
        existingOrder.OrderDate = request.OrderDate;
        existingOrder.CustomerName = request.CustomerName;
        existingOrder.Phone = request.Phone;

        var menuItemsResult = await orderService.GetMenuItemsByIdsAsync(request.Items, cancellationToken);
        if (!menuItemsResult.IsSuccess || !menuItemsResult.Value.Any())
        {
            return Error.BadRequest("Invalid menu items.");
        }

        existingOrder.OrderMenuItems.Clear();

        foreach (var menuItem in menuItemsResult.Value)
        {
            existingOrder.OrderMenuItems.Add(new OrderMenuItem
            {
                MenuItemId = menuItem.Id,
                OrderId = existingOrder.Id
            });
        }

        var updatedOrderResult = await orderService.UpdateOrderAsync(existingOrder, cancellationToken);

        return (updatedOrderResult.IsSuccess)
            ? updatedOrderResult.Value.ToDto()
            : Error.BadRequest("Update Failed");
    }
}