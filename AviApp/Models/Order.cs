using System;
using System.Collections.Generic;
using System.Linq;

namespace AviApp.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; } 
    public DateTime OrderDate { get; set; }
    public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    public decimal TotalAmount => Items.Sum(item => item.Price);
    
}