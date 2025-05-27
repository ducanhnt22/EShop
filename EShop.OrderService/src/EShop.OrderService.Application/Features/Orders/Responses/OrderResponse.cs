using EShop.OrderService.Domain.Entities;
using EShop.OrderService.Domain.Enums;

namespace EShop.OrderService.Application.Features.Orders.Responses;

public sealed record OrderResponse(
    Guid Id,
    string Message,
    Guid UserId,
    decimal TotalAmount,
    string ShippingAddress,
    string PhoneNumber,
    OrderStatus Status);
