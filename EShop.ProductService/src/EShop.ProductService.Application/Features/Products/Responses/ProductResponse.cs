namespace EShop.ProductService.Application.Features.Products.Responses;

public sealed record ProductResponse(
    Guid Id,
    string Message,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string ImageUrl,
    Guid CategoryId); 