using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers
{
    public static class OrderMapper
    {
     
        public static Order ToEntity(this OrderDto model, List<MenuItem> menuItems)
        {
            return new Order
            {
                Id = model.Id,
                Email = model.Email,
                OrderDate = model.OrderDate,
                CustomerName = model.CustomerName, 
                Phone = model.Phone,
                MenuItemName = string.Join(", ", menuItems.Select(m => m.Name)), 
                OrderMenuItems = menuItems.Select(m => new OrderMenuItem
                {
                    MenuItemId = m.Id,
                    OrderId = 0 
                }).ToList()
            };
        }
        
        public static OrderDto ToDto(this Order entity)
        {
            return new OrderDto
            {
                Id = entity.Id, 
                Email = entity.Email,
                OrderDate = entity.OrderDate,
                CustomerName = entity.CustomerName,
                Phone = entity.Phone,
                MenuItemName = entity.MenuItemName ?? "",

                OrderMenuItems = entity.OrderMenuItems?.Select(omi => new OrderMenuItemDto
                {
                    OrderId = omi.OrderId,
                    MenuItemId = omi.MenuItemId
                }).ToList() ?? new List<OrderMenuItemDto>()
            };
        }

    }
}