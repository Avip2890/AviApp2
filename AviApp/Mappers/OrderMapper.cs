using AviApp.Models;
using AviApp.Domain.Entities;

namespace AviApp.Mappers;

public static class OrderMapper
{
    // המרה מ-DTO ל-Entity
    public static Domain.Entities.Order ToEntity(this OrderDto model)
    {
        return new Domain.Entities.Order
        {
            Id = model.Id,
            CustomerId = model.CustomerId,
            OrderDate = model.OrderDate,
            Items = model.Items.Select(item => new Domain.Entities.MenuItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsAvailable = item.IsAvailable
            }).ToList()
        };
    }

    // המרה מ-Entity ל-DTO
    public static OrderDto ToDto(this Domain.Entities.Order entity)
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