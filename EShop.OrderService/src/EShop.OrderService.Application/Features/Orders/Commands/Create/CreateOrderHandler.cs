//using EShop.OrderService.Application.Common.Exceptions;
//using EShop.OrderService.Application.Features.Orders.Responses;
//using EShop.OrderService.Domain.Entities;
//using EShop.OrderService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
//using MediatR;

//namespace EShop.OrderService.Application.Features.Orders.Commands.Create;

//public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
//{
//    private readonly IUnitOfWorks _unitOfWorks;

//    public CreateOrderHandler(IUnitOfWorks unitOfWorks)
//    {
//        _unitOfWorks = unitOfWorks;
//    }

//    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
//    {
//        var orderItems = new List<OrderItem>();
//        decimal totalAmount = 0;

//        foreach (var item in request.OrderItems)
//        {
//            var product = await _unitOfWorks.ProductRepository.GetById(item.ProductId);
//            if (product == null)
//            {
//                throw new AppExceptions($"Product with ID {item.ProductId} not found");
//            }

//            if (product.StockQuantity < item.Quantity)
//            {
//                throw new AppExceptions($"Insufficient stock for product {product.Name}");
//            }

//            var orderItem = new OrderItem
//            {
//                Id = Guid.NewGuid(),
//                ProductId = product.Id,
//                ProductName = product.Name,
//                UnitPrice = product.Price,
//                Quantity = item.Quantity,
//                TotalPrice = product.Price * item.Quantity,
//                CreatedAt = DateTime.UtcNow
//            };

//            orderItems.Add(orderItem);
//            totalAmount += orderItem.TotalPrice;
//        }

//        var order = new Order
//        {
//            Id = Guid.NewGuid(),
//            UserId = request.UserId,
//            TotalAmount = totalAmount,
//            ShippingAddress = request.ShippingAddress,
//            PhoneNumber = request.PhoneNumber,
//            Status = OrderStatus.Pending,
//            OrderItems = orderItems,
//            CreatedAt = DateTime.UtcNow,
//            IsDeleted = false
//        };

//        await _unitOfWorks.OrderRepository.Add(order);
//        await _unitOfWorks.SaveChangesAsync();

//        return new OrderResponse(
//            order.Id,
//            "Order created successfully",
//            order.UserId,
//            order.TotalAmount,
//            order.ShippingAddress,
//            order.PhoneNumber,
//            order.Status,
//            order.OrderItems.Select(oi => new OrderItemResponse(
//                oi.Id,
//                oi.ProductId,
//                oi.ProductName,
//                oi.UnitPrice,
//                oi.Quantity,
//                oi.TotalPrice
//            )).ToList());
//    }
//} 