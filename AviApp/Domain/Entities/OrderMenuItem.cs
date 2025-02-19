using System;
using System.Collections.Generic;

namespace AviApp.Domain.Entities;

public partial class OrderMenuItem
{
    public int OrderId { get; set; }

    public int MenuItemId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual MenuItem MenuItem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
