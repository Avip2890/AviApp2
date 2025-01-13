using AviApp.Api.MenuItem.CreateMenuItem;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Mappers;

namespace AviApp.Api.MenuItem.MenuItemHandlers;

public class CreateMenuItemHandler(IMenuItemService menuItemService)
    : IRequestHandler<CreateMenuItemCommand, Result<MenuItemDto>>
{
    public async Task<Result<MenuItemDto>> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItemEntity = request.MenuItemDto.ToEntity();
        var result = await menuItemService.AddMenuItemAsync(menuItemEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<MenuItemDto>.Failure(result.Error);
        }

        return Result<MenuItemDto>.Success(result.Value.ToDto());
    }
}