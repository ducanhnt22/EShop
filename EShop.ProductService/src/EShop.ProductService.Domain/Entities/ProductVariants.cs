using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.ProductService.Domain.Common;

namespace EShop.ProductService.Domain.Entities
{
    public class ProductVariants : BaseAuditableEntity, ISoftDelete
    {
        public string VariantName { get; set; } = string.Empty;
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public ICollection<ProductItems> ProductItems { get; set; } = new List<ProductItems>();
        public bool IsDeleted { get; set; } = false;
    }
}