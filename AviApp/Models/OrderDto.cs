namespace AviApp.Models;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }

    public CustomerDto Customer { get; set; } = null!; // CustomerDto במקום Customer
    public ICollection<MenuItemDto> Items { get; set; } = new List<MenuItemDto>(); // MenuItemDto במקום MenuItem
}