using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

public static class OrderMapper
{
    public static Order ToEntity(this OrderDto model, List<MenuItem> menuItems)
    {
        return new Order
        {
            Id = model.Id ?? 0,
            CustomerId = model.CustomerId,
            OrderDate = model.OrderDate,
            /*OrderMenuItems = menuItems.Select(m => new OrderMenuItems 
            { 
                 = m.Id 
            }).ToList() // ✅ שימוש בטבלת החיבור*/
        };
    }

    public static OrderDto ToDto(this Order entity)
    {
        return new OrderDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId ?? 0,
            OrderDate = entity.OrderDate,
            OrderMenuItems = entity.OrderMenuItems
                .Select(omi => new OrderMenuItemDto
                {
                    OrderId = omi.OrderId,
                    MenuItemId = omi.MenuItemId
                }).ToList() 
        };
    }

}