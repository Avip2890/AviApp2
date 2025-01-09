

namespace AviApp.Domain.Entities;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
    
    public int? OrderId { get; set; } 
    public Order? Order { get; set; } 
}
