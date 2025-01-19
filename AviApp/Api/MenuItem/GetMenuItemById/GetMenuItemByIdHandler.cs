using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Mappers;

namespace AviApp.Api.MenuItem.MenuItemHandlers;

public class GetMenuItemByIdHandler(IMenuItemService menuItemService)
    : IRequestHandler<GetMenuItemByIdQuery, Result<MenuItemDto>>
{
    public async Task<Result<MenuItemDto>> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await menuItemService.GetMenuItemByIdAsync(request.Id, cancellationToken);
        if (!result.IsSuccess)
        {
            return Result<MenuItemDto>.Failure(result.Error);
        }

        return Result<MenuItemDto>.Success(result.Value.ToDto());
    }
}