using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, MenuItemDto?>
{
    private readonly IMenuItemService _menuItemService;

    public UpdateMenuItemCommandHandler(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    public async Task<MenuItemDto?> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
       
        var updatedMenuItemDto = new MenuItemDto
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IsAvailable = request.IsAvailable
        };

       
        var updatedMenuItem = await _menuItemService.UpdateMenuItemAsync(request.Id, updatedMenuItemDto, cancellationToken);

        return updatedMenuItem;
    }
}