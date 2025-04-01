using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Api.Orders.CreateOrder;

public class CreateOrderHandler(IOrderService orderService, ICustomerService customerService, AvipAppDbContext context)
    : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var createOrderRequest = request.CreateOrderRequest;

        var menuItemsResult = await orderService.GetMenuItemsByIdsAsync(
            createOrderRequest.OrderMenuItems.Select(x => x.MenuItemId),
            cancellationToken);

        if (!menuItemsResult.IsSuccess || !menuItemsResult.Value.Any())
        {
            return Error.BadRequest("Couldn't find menu items.");
        }

        var orderEntity = new Order
        {
            Email = createOrderRequest.Email,
            OrderDate = createOrderRequest.OrderDate.Date,
            CustomerName = createOrderRequest.CustomerName,
            Phone = createOrderRequest.Phone,
            MenuItemName = string.Join(", ", menuItemsResult.Value.Select(m => m.Name)),
            OrderMenuItems = menuItemsResult.Value.Select(m => new OrderMenuItem
            {
                MenuItemId = m.Id
            }).ToList()
        };

        await context.Orders.AddAsync(orderEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var savedOrder = await context.Orders
            .Include(o => o.OrderMenuItems)
            .FirstOrDefaultAsync(o => o.Id == orderEntity.Id, cancellationToken);

        if (savedOrder == null)
        {
            return Error.BadRequest("Failed to retrieve saved order.");
        }

        return savedOrder.ToDto(); // ✅ עכשיו חוזר עם ID אמיתי ופריטים
    }
}