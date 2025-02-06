using AviApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Domain.Context;

public class AvipAppDbContext(DbContextOptions<AvipAppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderMenuItems> OrderMenuItems { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🔹 הגדרת טבלת הקישור OrderMenuItems עם מפתח משולב
        modelBuilder.Entity<OrderMenuItems>()
            .HasKey(omi => new { omi.OrderId, omi.MenuItemId });

        // 🔹 קשר בין Order ל- OrderMenuItems (Cascade Delete)
        modelBuilder.Entity<OrderMenuItems>()
            .HasOne(omi => omi.Order)
            .WithMany(o => o.OrderMenuItems)
            .HasForeignKey(omi => omi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 קשר בין MenuItem ל- OrderMenuItems (Cascade Delete)
        modelBuilder.Entity<OrderMenuItems>()
            .HasOne(omi => omi.MenuItem)
            .WithMany(m => m.OrderMenuItems)
            .HasForeignKey(omi => omi.MenuItemId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 Order: ודא ששדות חובה לא יכולים להיות NULL
        modelBuilder.Entity<Order>()
            .Property(o => o.OrderDate)
            .IsRequired()
            .HasColumnType("datetime");

        modelBuilder.Entity<Order>()
            .Property(o => o.CustomerId)
            .IsRequired();

        // 🔹 Customer: ודא ששם הלקוח לא יכול להיות NULL
        modelBuilder.Entity<Customer>()
            .Property(c => c.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        // 🔹 MenuItem: ודא שמחיר לא ייחתך
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Price)
            .HasPrecision(18, 2);
    }
}