using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

public class UpdateMenuItemCommandHandler(IMenuItemService menuItemService)
    : IRequestHandler<UpdateMenuItemCommand, MenuItemDto?>
{
    public async Task<MenuItemDto?> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var existingMenuItemResult = await menuItemService.GetMenuItemByIdAsync(request.Id, cancellationToken);

        if (!existingMenuItemResult.IsSuccess || existingMenuItemResult.Value == null)
        {
            return null; 
        }

        var existingMenuItem = existingMenuItemResult.Value;

        existingMenuItem.Name = request.Name;
        existingMenuItem.Description = request.Description;
        existingMenuItem.Price = request.Price;
        existingMenuItem.IsAvailable = request.IsAvailable;
        
        var updatedMenuItemResult = await menuItemService.UpdateMenuItemAsync(request.Id, existingMenuItem, cancellationToken);

        if (!updatedMenuItemResult.IsSuccess || updatedMenuItemResult.Value == null)
        {
            return null; 
        }
        
        return updatedMenuItemResult.Value.ToDto();
    }
}