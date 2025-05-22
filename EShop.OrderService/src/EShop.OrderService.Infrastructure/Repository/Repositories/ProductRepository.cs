using EShop.OrderService.Infrastructure.Repository.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace EShop.OrderService.Infrastructure.Repository.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _productServiceUrl;

    public ProductRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _productServiceUrl = configuration["ServiceUrls:ProductService"];
    }

    public async Task<(string Name, decimal Price, int StockQuantity)> GetProductInfo(Guid productId)
    {
        var response = await _httpClient.GetFromJsonAsync<ProductDto>($"{_productServiceUrl}/api/products/{productId}");
        if (response == null)
        {
            throw new Exception($"Product with ID {productId} not found");
        }
        return (response.Name, response.Price, response.StockQuantity);
    }

    public async Task UpdateStockQuantity(Guid productId, int quantity)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"{_productServiceUrl}/api/products/{productId}/stock",
            new { Quantity = quantity });

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to update stock quantity for product {productId}");
        }
    }
}

internal class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
} 