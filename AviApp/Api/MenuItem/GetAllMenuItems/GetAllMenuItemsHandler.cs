using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Mappers;

namespace AviApp.Api.MenuItem.GetAllMenuItems;

public class GetAllMenuItemsHandler(IMenuItemService menuItemService)
    : IRequestHandler<GetAllMenuItemsQuery, Result<List<MenuItemDto>>>
{
    public async Task<Result<List<MenuItemDto>>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var result = await menuItemService.GetAllMenuItemsAsync(cancellationToken);
        return result.IsSuccess
            ? result.Value.Select(m => m.ToDto()).ToList()
            : Error.BadRequest("Menu item Service returned no results");
    }
}