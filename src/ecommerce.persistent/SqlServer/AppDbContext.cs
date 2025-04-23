using ecommerce.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.persistence.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Category).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.StockQuantity).HasDefaultValue(0);
            });

            modelBuilder.Entity<Product>().HasData(
            new Product(
                id: Guid.Parse("a1b2c3d4-1234-5678-9012-345678901234"),
                name: "iPhone 15 Pro",
                description: "Flagship smartphone",
                price: 1299.99m,
                stockQuantity: 100,
                category: "Điện tử"

            ),
            new Product(
                id: Guid.Parse("b2c3d4e5-2345-6789-0123-456789012345"),
                name: "MacBook Pro M2",
                description: "Laptop cao cấp",
                price: 1999.99m,
                stockQuantity: 50,
                category: "Máy tính"
            ));
        }
    }
}
