using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

public record UpdateMenuItemCommand(int Id, string Name, decimal Price, string Description, bool IsAvailable) 
    : IRequest<Result<MenuItemDto>>;