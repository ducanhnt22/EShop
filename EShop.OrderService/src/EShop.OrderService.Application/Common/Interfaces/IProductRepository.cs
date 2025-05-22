namespace EShop.OrderService.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<(string Name, decimal Price, int StockQuantity)> GetProductInfo(Guid productId);
    Task UpdateStockQuantity(Guid productId, int quantity);
} 