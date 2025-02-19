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
        // בדוק אם יש את כל פרטי המנות
        var menuItemsResult = await orderService.GetMenuItemsByIdsAsync(request.Items, cancellationToken);
    
        if (!menuItemsResult.IsSuccess || !menuItemsResult.Value.Any())
        {
            return Error.BadRequest("Couldn't find menu items.");
        }


        // יצירת הזמנה וקשירת המנות לה
        var orderEntity = new Order
        {
            CustomerId = request.CustomerId,
            OrderDate = request.OrderDate ?? DateTime.UtcNow,
        };

        // שמירת ההזמנה ב-DB
        var result = await orderService.CreateOrderAsync(orderEntity, cancellationToken);

        // אם שמירת ההזמנה הצליחה, החזר את הנתונים
        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
}
