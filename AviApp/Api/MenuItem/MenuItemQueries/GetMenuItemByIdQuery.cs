using MediatR;
using AviApp.Models;
using AviApp.Results;

namespace AviApp.Api.MenuItem.MenuItemQueries;

public record GetMenuItemByIdQuery(int Id) : IRequest<Result<MenuItemDto>>;