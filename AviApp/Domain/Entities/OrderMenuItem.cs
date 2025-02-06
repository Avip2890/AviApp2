namespace AviApp.Domain.Entities;

public class OrderMenuItems
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;
}