using EShop.ProductService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductVariants> ProductVariants { get; set; }
    public DbSet<ProductItems> ProductItems { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
} 