
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Mappers;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemHandler(IMenuItemService menuItemService)
    : IRequestHandler<CreateMenuItemCommand, Result<MenuItemDto>>
{
    public async Task<Result<MenuItemDto>> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItemEntity = new Domain.Entities.MenuItem
        {
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            IsAvailable = request.IsAvailable,
          
        };
        var result = await menuItemService.AddMenuItemAsync(menuItemEntity, cancellationToken);

        return result.IsSuccess 
            ? result.Value.ToDto() 
            : Error.BadRequest("Failed to create menu item");
    }
}