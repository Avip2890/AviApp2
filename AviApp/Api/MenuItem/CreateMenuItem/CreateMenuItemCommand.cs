using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public record CreateMenuItemCommand(MenuItemDto CreateMenuItemRequest) : IRequest<Result<MenuItemDto>>;