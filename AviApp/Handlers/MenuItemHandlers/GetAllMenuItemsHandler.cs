/*using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.MenuItemQueries;
using MediatR;

namespace AviApp.Handlers.MenuItemHandlers;

public class GetAllMenuItemsHandler : IRequestHandler<GetAllMenuItemsQuery, IEnumerable<MenuItem>>
{
    private readonly IMenuItemService _menuItemService;

    public GetAllMenuItemsHandler(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    public Task<IEnumerable<MenuItem>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_menuItemService.GetAllMenuItems());
    }
}*/