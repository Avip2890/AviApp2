using AviApp.Models;
using MediatR;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

public abstract class UpdateMenuItemCommand : IRequest<MenuItemDto?>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}