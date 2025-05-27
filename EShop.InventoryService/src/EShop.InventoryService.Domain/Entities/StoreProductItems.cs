using EShop.Shared.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.InventoryService.Domain.Entities
{
    public class StoreProductItems : BaseAuditableEntity, IsSoftDelete
    {
        public Guid StoreId { get; set; }
        public Guid ProductItemId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Stock { get; set; }
        public ICollection<StoreProductItemsOrder> StoreProductItemsOrders { get; set; } = new List<StoreProductItemsOrder>();
        public bool IsSoftDelete { get; set; } = false;
    }
}