using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public abstract class CreateMenuItemCommand(MenuItemDto menuItemDto) : IRequest<MenuItemDto>
{
    public MenuItemDto MenuItemDto { get; set; } = menuItemDto;
}