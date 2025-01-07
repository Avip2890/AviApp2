using MediatR;
using AviApp.Models;

namespace AviApp.Api.MenuItem.MenuItemQueries;

public class GetMenuItemByIdQuery(int id) : IRequest<MenuItemDto?>
{
    public int Id { get; set; } = id;
}