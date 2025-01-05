using AviApp.Models;
using AviApp.Domain.Entities;

namespace AviApp.Mappers;

public static class OrderMapper
{
 
    public static Order ToEntity(this OrderDto model)
    {
        return new Order
        {
            Id = model.Id,
            CustomerId = model.CustomerId,
            OrderDate = model.OrderDate,
            Items = model.Items.Select(item => new MenuItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsAvailable = item.IsAvailable
            }).ToList()
        };
    }

  
    public static OrderDto ToDto(this Order entity)
    {
        return new OrderDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            OrderDate = entity.OrderDate,
            Items = entity.Items.Select(item => new MenuItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsAvailable = item.IsAvailable
            }).ToList()
        };
    }
}