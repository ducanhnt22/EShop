namespace EShop.OrderService.Domain.Enums;

public enum OrderStatus
{
    All = -1,
    Pending = 0,
    StoreConfirmed = 1,
    Processing = 2,
    Shipping = 3,
    Completed = 4,
    Cancelled = 5,
    Mixed = 6
} 