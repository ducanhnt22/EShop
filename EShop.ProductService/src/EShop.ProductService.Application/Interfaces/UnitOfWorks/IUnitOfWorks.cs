using EShop.ProductService.Application.Common.Interfaces;

namespace EShop.ProductService.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWorks
{
    public IProductRepository ProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public Task<int> SaveChangesAsync();
}
