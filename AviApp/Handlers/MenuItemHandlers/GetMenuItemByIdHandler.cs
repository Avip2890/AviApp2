using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.MenuItemQueries;
using MediatR;

namespace AviApp.Handlers.MenuItemHandlers;

public class GetMenuItemByIdHandler : IRequestHandler<GetMenuItemByIdQuery, MenuItem?>
{
    private readonly IMenuItemService _menuItemService;

    public GetMenuItemByIdHandler(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    public Task<MenuItem?> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_menuItemService.GetMenuItemById(request.Id));
    }
}