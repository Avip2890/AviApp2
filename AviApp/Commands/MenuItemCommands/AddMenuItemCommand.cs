using MediatR;

namespace AviApp.Commands.MenuItemCommands;

public class AddMenuItemCommand : IRequest<Models.MenuItem>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}