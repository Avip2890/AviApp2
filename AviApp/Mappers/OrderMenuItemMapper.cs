using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

public static class OrderMenuItemMapper
{
    public static OrderMenuItems ToEntity(this OrderMenuItemDto model)
    {
        return new OrderMenuItems
        {
            OrderId = model.OrderId,
            MenuItemId = model.MenuItemId
        };
    }

    public static OrderMenuItemDto ToDto(this OrderMenuItems entity)
    {
        return new OrderMenuItemDto
        {
            OrderId = entity.OrderId,
            MenuItemId = entity.MenuItemId
        };
    }
}