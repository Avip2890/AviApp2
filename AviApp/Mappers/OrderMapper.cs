using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

public static class OrderMapper
{
    public static Order ToEntity(this OrderDto dto, ICollection<MenuItem> menuItems)
    {
        return new Order
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId,
            OrderDate = dto.OrderDate,
            Items = menuItems // פריטים שנשלפו מה-DB בהתבסס על מזהים
        };
    }

    public static OrderDto ToDto(this Order entity)
    {
        return new OrderDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            OrderDate = entity.OrderDate,
            Items = entity.Items.Select(item => item.Id).ToList() // החזרת מזהים בלבד
        };
    }
}