using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

public static class MenuItemMapper
{
    public static MenuItem ToEntity(this MenuItemDto model)
    {
        return new MenuItem
        {
            Id = model.Id ?? 0,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            IsAvailable = model.IsAvailable
        };
    }

    public static MenuItemDto ToDto(this MenuItem entity)
    {
        return new MenuItemDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            IsAvailable = entity.IsAvailable
        };
    }
}