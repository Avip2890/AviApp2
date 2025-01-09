using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.MenuItem.MenuItemHandlers;

public class GetMenuItemByIdHandler(IMenuItemService menuItemService)
    : IRequestHandler<GetMenuItemByIdQuery, MenuItemDto?>
{
    public async Task<MenuItemDto?> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
      
        var result = await menuItemService.GetMenuItemByIdAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
           
            throw new InvalidOperationException(result.Error);
        }

      
        return result.Value.ToDto();
    }
}