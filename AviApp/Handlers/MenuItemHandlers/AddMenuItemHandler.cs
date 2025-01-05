/*using AviApp.Commands.MenuItemCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.MenuItemHandlers;

public class AddMenuItemHandler : IRequestHandler<AddMenuItemCommand, Models.MenuItem>
{
    private readonly IMenuItemService _menuItemService;

    public AddMenuItemHandler(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    public Task<Models.MenuItem> Handle(AddMenuItemCommand request, CancellationToken cancellationToken)
    {
        var newMenuItem = new Models.MenuItem
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IsAvailable = request.IsAvailable
        };

        return Task.FromResult<Models.MenuItem>(_menuItemService.AddMenuItem(newMenuItem));

    }
}*/