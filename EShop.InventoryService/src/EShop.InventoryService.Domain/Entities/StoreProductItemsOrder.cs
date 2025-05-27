using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.InventoryService.Domain.Common;

namespace EShop.InventoryService.Domain.Entities
{
    public class StoreProductItemsOrder : BaseAuditableEntity, ISoftDelete
    {
        public Guid StoreProductItemsId { get; set; }
        public StoreProductItems StoreProductItems { get; set; }
        public Guid StoreOrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}