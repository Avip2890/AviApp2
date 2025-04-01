using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

public class UpdateMenuItemHandler(IMenuItemService menuItemService)
    : IRequestHandler<UpdateMenuItemCommand, Result<MenuItemDto>>
{
    public async Task<Result<MenuItemDto>> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var existingMenuItemResult = await menuItemService.GetMenuItemByIdAsync(request.Id, cancellationToken);

        if (!existingMenuItemResult.IsSuccess)
        {
            return Error.BadRequest($"Menu item with ID {request.Id} was not found.");
        }
        var menuItemEntity = existingMenuItemResult.Value;
        menuItemEntity.Name = request.Name;
        menuItemEntity.Price = request.Price;
        menuItemEntity.Description = request.Description;
        menuItemEntity.IsAvailable = request.IsAvailable;
        menuItemEntity.ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl)
            ? menuItemEntity.ImageUrl
            : request.ImageUrl;
  

        var result = await menuItemService.UpdateMenuItemAsync(request.Id, menuItemEntity, cancellationToken);

        return result.IsSuccess 
            ? Result<MenuItemDto>.Success(result.Value.ToDto()) 
            : Error.BadRequest("Could not update MenuItem");
    }
}