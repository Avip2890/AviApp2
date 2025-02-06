namespace AviApp.Domain.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    public ICollection<OrderMenuItems> OrderMenuItems { get; set; } = new List<OrderMenuItems>();
}