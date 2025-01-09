namespace AviApp.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>(); // קשר ל-MenuItem מ-Domain.Entities
}