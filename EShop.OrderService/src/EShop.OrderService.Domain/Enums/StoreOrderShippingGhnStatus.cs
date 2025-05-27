namespace EShop.OrderService.Domain.Enums;

public enum StoreOrderShippingGhnStatus
{
    All = -1,
    Pending = 0,
    ReadyToPick = 1,
    Picking = 2,
    Cancelled = 3,
    MoneyCollectedPicking = 4,
    Picked = 5,
    Storing = 6,
    Transporting = 7,
    Sorting = 8,
    Delivering = 9,
    MoneyCollectedDelivering = 10,
    Delivered = 11,
    DeliveredFailed = 12,
    WaitingForReturn = 13,
    Return = 14,
    ReturnTransporting = 15,
    ReturnSorting = 16,
    Returning = 17,
    ReturnFailed = 18,
    Returned = 19,
    Exception = 20,
    Damaged = 21,
    Lost = 22
}
