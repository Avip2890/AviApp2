
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Mappers;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemHandler(IMenuItemService menuItemService)
    : IRequestHandler<CreateMenuItemCommand, Result<MenuItemDto>>
{
    public async Task<Result<MenuItemDto>> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItemEntity = request.MenuItemDto.ToEntity();
        var result = await menuItemService.AddMenuItemAsync(menuItemEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return Error.BadRequest("Failed to create menu item");
        }

        return Result<MenuItemDto>.Success(result.Value.ToDto());
    }
}