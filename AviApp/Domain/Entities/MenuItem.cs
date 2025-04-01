using System;
using System.Collections.Generic;

namespace AviApp.Domain.Entities;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<OrderMenuItem> OrderMenuItems { get; set; } = new List<OrderMenuItem>();
}
