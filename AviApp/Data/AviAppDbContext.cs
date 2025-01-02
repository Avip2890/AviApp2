using AviApp.Models;

namespace AviApp.Data;

using Microsoft.EntityFrameworkCore;

public class AviAppDbContext : DbContext
{
    public AviAppDbContext(DbContextOptions<AviAppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
}
