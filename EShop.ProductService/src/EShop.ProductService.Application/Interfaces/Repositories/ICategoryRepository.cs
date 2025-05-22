using EShop.ProductService.Domain.Entities;

namespace EShop.ProductService.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category> GetById(Guid id);
    Task<IEnumerable<Category>> GetAll();
    Task Add(Category category);
    void Update(Category category);
    void Delete(Category category);
    Task<bool> Exists(Guid id);
}
