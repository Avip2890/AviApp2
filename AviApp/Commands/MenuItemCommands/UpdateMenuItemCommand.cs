using MediatR;

namespace AviApp.Commands.MenuItemCommands;

public class UpdateMenuItemCommand : IRequest<Models.MenuItem?>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}