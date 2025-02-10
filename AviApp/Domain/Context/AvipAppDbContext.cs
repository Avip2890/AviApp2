using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AviApp.Domain.Entities;

namespace AviApp.Domain.Context
{
    public class AvipAppDbContext(DbContextOptions<AvipAppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenuItems> OrderMenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<OrderMenuItems>()
                .HasKey(omi => new { omi.OrderId, omi.MenuItemId });

            modelBuilder.Entity<OrderMenuItems>()
                .HasOne(omi => omi.Order)
                .WithMany(o => o.OrderMenuItems)
                .HasForeignKey(omi => omi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderMenuItems>()
                .HasOne(omi => omi.MenuItem)
                .WithMany(m => m.OrderMenuItems)
                .HasForeignKey(omi => omi.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnType("datetime");

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AvipAppDbContext>
    {
        public AvipAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AvipAppDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new AvipAppDbContext(optionsBuilder.Options);
        }
    }
}
