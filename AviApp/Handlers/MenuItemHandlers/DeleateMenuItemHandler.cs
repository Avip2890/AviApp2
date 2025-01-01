using AviApp.Commands.MenuItemCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.MenuItemHandlers;

public class DeleateMenuItemHandler :  IRequestHandler<DeleteMenuItemCommand,bool>
{
    private readonly IMenuItemService _menuItemService;
    
    public DeleateMenuItemHandler(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }
    
    public Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
    {
        var result = _menuItemService.DeleteMenuItem(request.Id);
        return Task.FromResult(result);
    }
}