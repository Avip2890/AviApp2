namespace AviApp.Models;

public class OrderDto
{
    public int? Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }

    public List<OrderMenuItemDto> OrderMenuItems { get; set; } = new(); 
}