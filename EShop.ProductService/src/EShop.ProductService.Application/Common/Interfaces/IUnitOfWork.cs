namespace EShop.ProductService.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task<int> SaveChangesAsync();
} 