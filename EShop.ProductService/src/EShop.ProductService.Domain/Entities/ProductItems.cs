using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.ProductService.Domain.Common;

namespace EShop.ProductService.Domain.Entities
{
    public class ProductItems : BaseAuditableEntity, ISoftDelete
    {
        public string SKU { get; set; } = string.Empty;
        public int Weight { get; set; }
        public Guid ProductVariantId { get; set; }
        public ProductVariants ProductVariant { get; set; }
        public ICollection<ProductImages> ProductImages { get; set; } = new List<ProductImages>();
        public bool IsDeleted { get; set; } = false;
    }
}