using System;
using EShop.ProductService.Domain.Common;

namespace EShop.ProductService.Domain.Entities;

public class Product : BaseAuditableEntity, ISoftDelete
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<ProductVariants> ProductVariants { get; set; } = new List<ProductVariants>();
} 
