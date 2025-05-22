using EShop.ProductService.Application.Features.Products.Responses;
using MediatR;

namespace EShop.ProductService.Application.Features.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string ImageUrl,
    Guid CategoryId) : IRequest<ProductResponse>; 