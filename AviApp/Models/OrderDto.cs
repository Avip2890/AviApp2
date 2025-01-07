namespace AviApp.Models;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }

   
    public ICollection<int> Items { get; set; } = new List<int>();
}