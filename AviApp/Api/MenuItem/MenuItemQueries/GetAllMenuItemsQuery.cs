using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.MenuItemQueries;

public record GetAllMenuItemsQuery() : IRequest<Result<List<MenuItemDto>>>;