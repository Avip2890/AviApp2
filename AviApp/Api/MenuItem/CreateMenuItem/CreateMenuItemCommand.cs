using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public record CreateMenuItemCommand(MenuItemDto MenuItemDto) : IRequest<Result<MenuItemDto>>;
