using Microsoft.EntityFrameworkCore;
using SqlQueryApp.Models;

namespace SqlQueryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.Name);
            });

            // Seed sample data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "High-performance laptop for work and gaming",
                    Price = 1299.99m,
                    Stock = 15,
                    CreatedDate = DateTime.Now.AddDays(-30),
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartphone",
                    Description = "Latest model smartphone with advanced features",
                    Price = 899.99m,
                    Stock = 25,
                    CreatedDate = DateTime.Now.AddDays(-20),
                    IsActive = true
                },
                new Product
                {
                    Id = 3,
                    Name = "Wireless Headphones",
                    Description = "Premium wireless headphones with noise cancellation",
                    Price = 299.99m,
                    Stock = 50,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    IsActive = true
                }
            );
        }
    }
}