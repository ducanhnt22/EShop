using EShop.ProductService.Domain.Entities;

namespace EShop.ProductService.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product> GetById(Guid id);
    Task<IEnumerable<Product>> GetAll();
    Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId);
    Task Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task<bool> Exists(Guid id);
} 