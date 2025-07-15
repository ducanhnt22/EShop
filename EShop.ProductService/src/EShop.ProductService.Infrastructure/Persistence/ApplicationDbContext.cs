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
        
        // Configure Product entity with performance optimizations
        modelBuilder.Entity<Product>(entity =>
        {
            // Add indexes for frequently queried fields
            entity.HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");
            
            entity.HasIndex(p => p.CategoryId)
                .HasDatabaseName("IX_Products_CategoryId");
            
            entity.HasIndex(p => p.CreatedAt)
                .HasDatabaseName("IX_Products_CreatedAt");
            
            entity.HasIndex(p => p.Price)
                .HasDatabaseName("IX_Products_Price");
            
            entity.HasIndex(p => p.StockQuantity)
                .HasDatabaseName("IX_Products_StockQuantity");
            
            // Composite index for common query patterns
            entity.HasIndex(p => new { p.CategoryId, p.CreatedAt })
                .HasDatabaseName("IX_Products_CategoryId_CreatedAt");
        });
        
        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Categories_Name");
            
            entity.HasIndex(c => c.CreatedAt)
                .HasDatabaseName("IX_Categories_CreatedAt");
        });
        
        // Configure ProductVariants entity
        modelBuilder.Entity<ProductVariants>(entity =>
        {
            entity.HasIndex(pv => pv.ProductId)
                .HasDatabaseName("IX_ProductVariants_ProductId");
        });
        
        // Configure ProductItems entity
        modelBuilder.Entity<ProductItems>(entity =>
        {
            entity.HasIndex(pi => pi.ProductId)
                .HasDatabaseName("IX_ProductItems_ProductId");
        });
        
        // Configure ProductImages entity
        modelBuilder.Entity<ProductImages>(entity =>
        {
            entity.HasIndex(pi => pi.ProductId)
                .HasDatabaseName("IX_ProductImages_ProductId");
        });
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Performance optimizations
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.EnableServiceProviderCaching(true);
            optionsBuilder.EnableDetailedErrors(false);
        }
    }
} 