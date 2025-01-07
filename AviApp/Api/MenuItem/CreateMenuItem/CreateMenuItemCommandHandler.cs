using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, MenuItemDto>
{
    private readonly IMenuItemService _menuItemService;

    public CreateMenuItemCommandHandler(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    public async Task<MenuItemDto> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
     
        var createdMenuItem = await _menuItemService.AddMenuItemAsync(request.MenuItemDto, cancellationToken);

       
        return createdMenuItem;
    }
}