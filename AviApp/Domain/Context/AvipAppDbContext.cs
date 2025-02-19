using System;
using System.Collections.Generic;
using AviApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Domain.Context;

public partial class AvipAppDbContext : DbContext
{
    public AvipAppDbContext(DbContextOptions<AvipAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderMenuItem> OrderMenuItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0729B8E63E");

            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.MenuItemName).HasMaxLength(100);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<OrderMenuItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.MenuItemId });

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderMenuItems)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderMenuItems_Customers");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderMenuItems)
                .HasForeignKey(d => d.MenuItemId)
                .HasConstraintName("FK_OrderMenuItems_MenuItem");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderMenuItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderMenuItems_Order");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC075BC8C636");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC070D19B779");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Roles"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD8EE79B57");
                        j.ToTable("UserRoles");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
