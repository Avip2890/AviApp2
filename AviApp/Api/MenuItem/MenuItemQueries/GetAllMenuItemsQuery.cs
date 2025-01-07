using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.MenuItemQueries;

public class GetAllMenuItemsQuery : IRequest<IEnumerable<MenuItemDto>>
{
}