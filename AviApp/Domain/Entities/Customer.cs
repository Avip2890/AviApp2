using System;
using System.Collections.Generic;

namespace AviApp.Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<OrderMenuItem> OrderMenuItems { get; set; } = new List<OrderMenuItem>();
}
