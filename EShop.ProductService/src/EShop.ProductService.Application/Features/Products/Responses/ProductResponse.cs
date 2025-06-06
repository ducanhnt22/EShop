namespace EShop.ProductService.Application.Features.Products.Responses;

public record GetProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedDate { get; set; }
}; 