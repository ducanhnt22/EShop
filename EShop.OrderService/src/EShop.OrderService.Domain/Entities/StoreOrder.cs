using EShop.OrderService.Domain.Common;
using EShop.OrderService.Domain.Enums;

namespace EShop.OrderService.Domain.Entities;

public class StoreOrder : BaseAuditableEntity, ISoftDelete
{
    public Guid StoreId { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public StoreOrderStatus Status { get; set; }
    public StoreOrderShippingGhnStatus ShippingStatus { get; set; }
    public double TotalAmount { get; set; }
    public bool IsDeleted { get; set; } = false;
}
