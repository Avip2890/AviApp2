using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.MenuItem.MenuItemHandlers;

public class GetAllMenuItemsHandler(IMenuItemService menuItemService)
    : IRequestHandler<GetAllMenuItemsQuery, IEnumerable<MenuItemDto>>
{
    public async Task<IEnumerable<MenuItemDto>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var result = await menuItemService.GetAllMenuItemsAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Error);
        }

        // המרת הרשימה ל-DTO
        return result.Value.Select(menuItem => menuItem.ToDto());
    }
}