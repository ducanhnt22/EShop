using EShop.OrderService.Domain.Entities;

namespace EShop.OrderService.Application.Features.Orders.Responses;

public sealed record OrderResponse(
    Guid Id,
    string Message,
    Guid UserId,
    decimal TotalAmount,
    string ShippingAddress,
    string PhoneNumber,
    OrderStatus Status,
    List<OrderItemResponse> OrderItems);

public sealed record OrderItemResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice); 