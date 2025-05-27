using System;
using System.Collections.Generic;
using EShop.ProductService.Domain.Common;

namespace EShop.ProductService.Domain.Entities;

public class Category : BaseAuditableEntity, ISoftDelete
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; }
    public bool IsDeleted { get; set; } = false;
} 