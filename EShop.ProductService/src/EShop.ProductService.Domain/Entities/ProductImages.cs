using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.ProductService.Domain.Common;

namespace EShop.ProductService.Domain.Entities
{
    public class ProductImages : BaseAuditableEntity, ISoftDelete
    {
        public string ImageUrl { get; set; } = string.Empty;
        public Guid ProductItemId { get; set; }
        public ProductItems ProductItem { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}