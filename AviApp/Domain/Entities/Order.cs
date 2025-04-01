using System;
using System.Collections.Generic;

namespace AviApp.Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string? CustomerName { get; set; }

    public string? Phone { get; set; }

    public string? MenuItemName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<OrderMenuItem> OrderMenuItems { get; set; } = new List<OrderMenuItem>();
}
