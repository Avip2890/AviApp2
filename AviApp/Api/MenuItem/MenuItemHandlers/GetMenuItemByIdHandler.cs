using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.MenuItemHandlers;

public class GetMenuItemByIdHandler(IMenuItemService menuItemService)
    : IRequestHandler<GetMenuItemByIdQuery, MenuItemDto?>
{
    public async Task<MenuItemDto?> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
        // קריאה לשירות בצורה אסינכרונית
        return await menuItemService.GetMenuItemByIdAsync(request.Id, cancellationToken);
    }
}