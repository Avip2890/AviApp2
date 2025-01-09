using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemCommandHandler(IMenuItemService menuItemService)
    : IRequestHandler<CreateMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var result = await menuItemService.AddMenuItemAsync(request.MenuItemDto.ToEntity(), cancellationToken);
        
        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Error);
        }

        return result.Value.ToDto();
    }
}