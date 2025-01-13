using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

public record UpdateMenuItemCommand(int Id, MenuItemDto MenuItemDto) : IRequest<Result<MenuItemDto>>;
