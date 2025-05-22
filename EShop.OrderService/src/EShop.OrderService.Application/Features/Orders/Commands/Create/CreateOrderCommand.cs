using EShop.OrderService.Application.Features.Orders.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace EShop.OrderService.Application.Features.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    string ShippingAddress,
    string PhoneNumber,
    List<OrderItemDto> OrderItems) : IRequest<OrderResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}

public record OrderItemDto(
    Guid ProductId,
    int Quantity); 