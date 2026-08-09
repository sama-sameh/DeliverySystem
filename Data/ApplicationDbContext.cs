using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models.Entities;

namespace MyFirstProject.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Delivery>(entity =>
    {
        entity.HasOne(d => d.order)
              .WithMany() // أو .WithOne() حسب تصميمك
              .HasForeignKey(d => d.OrderId)
              .OnDelete(DeleteBehavior.Restrict); // يمنع خطأ Multiple Cascade Paths

        entity.HasOne(d => d.driver)
              .WithMany()
              .HasForeignKey(d => d.DriverId)
              .OnDelete(DeleteBehavior.Restrict);
    });
}

    }
}