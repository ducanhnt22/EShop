namespace EShop.OrderService.Domain.Enums;

public enum StoreOrderStatus
{
     All = -1,
    Pending = 0,           // Vừa tạo, chờ xử lý
    Confirmed = 1,         // Shop đã xác nhận đơn
    Preparing = 2,         // Đang đóng gói hàng
    ReadyToShip = 3,       // Đóng gói xong, chờ shipper lấy hàng
    Shipping = 4,          // Đang giao
    Delivered = 5,         // Giao thành công
    Cancelled = 6,         // Hủy toàn bộ đơn của store
    Returned = 7           // Trả hàng (toàn bộ)
}
