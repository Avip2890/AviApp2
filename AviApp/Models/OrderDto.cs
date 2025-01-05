

namespace AviApp.Models;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<MenuItemDto> Items { get; set; } = new List<MenuItemDto>();
    public decimal TotalAmount => Items.Sum(item => item.Price);
}