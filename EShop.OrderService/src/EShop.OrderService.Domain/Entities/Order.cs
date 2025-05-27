using System;
using System.Collections.Generic;
using EShop.OrderService.Domain.Common;
using EShop.OrderService.Domain.Enums;

namespace EShop.OrderService.Domain.Entities;

public class Order : BaseAuditableEntity, ISoftDelete
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public bool IsDeleted { get; set; } = false;
}

