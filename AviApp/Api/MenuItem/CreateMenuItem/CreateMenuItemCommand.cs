using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemCommand : IRequest<MenuItemDto>
{
    public MenuItemDto MenuItemDto { get; set; }

    public CreateMenuItemCommand(MenuItemDto menuItemDto)
    {
        MenuItemDto = menuItemDto;
    }
}