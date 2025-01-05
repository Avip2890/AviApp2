/*using AviApp.Commands.MenuItemCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.MenuItemHandlers;

public class UpdateMenuItemHandler : IRequestHandler<UpdateMenuItemCommand, Models.MenuItem?>

{
    private readonly IMenuItemService _menuItemService;
    
    public UpdateMenuItemHandler(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

    public Task<Models.MenuItem?> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var updatedMenuItem = new Models.MenuItem
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IsAvailable = request.IsAvailable
        };

        return Task.FromResult(_menuItemService.UpdateMenuItem(request.Id, updatedMenuItem));
    }
}*/