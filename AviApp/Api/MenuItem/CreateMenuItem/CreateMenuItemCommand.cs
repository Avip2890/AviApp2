using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public record CreateMenuItemCommand(string Name, string Description, decimal Price,bool IsAvailable ) : IRequest<Result<MenuItemDto>>;