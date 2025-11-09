using b2b_shop_demo_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace b2b_shop_demo_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<PurchaseLimit> PurchaseLimits => Set<PurchaseLimit>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            // Seed ساده برای دمو
            modelBuilder.Entity<Company>().HasData(new Company { Id = 1, Name = "Demo Co", Code = "DEMO" });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "demo",
                PasswordHash = "demo", // فقط برای دمو، بعداً درستش می‌کنیم
                CompanyId = 1
            });
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 50000000 },
                new Product { Id = 2, Name = "Monitor", Price = 15000000 }
            );
            modelBuilder.Entity<PurchaseLimit>().HasData(
                new PurchaseLimit { Id = 1, CompanyId = 1, MonthlyLimitAmount = 100000000 }
            );
        }
    }
}
