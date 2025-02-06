namespace AviApp.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public ICollection<OrderMenuItems> OrderMenuItems { get; set; } = new List<OrderMenuItems>(); // ✅ קשר מקשר בין הזמנות למנות
}