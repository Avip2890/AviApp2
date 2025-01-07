using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.MenuItemHandlers;

public class GetAllMenuItemsHandler(IMenuItemService menuItemService)
    : IRequestHandler<GetAllMenuItemsQuery, IEnumerable<MenuItemDto>>
{
    public async Task<IEnumerable<MenuItemDto>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
    {
       
        return await menuItemService.GetAllMenuItemsAsync(cancellationToken);
    }
}