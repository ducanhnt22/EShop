using EShop.ProductService.Application.Common.Interfaces;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Infrastructure.Persistence;
using EShop.ProductService.Infrastructure.Repository.Repositories;

namespace EShop.ProductService.Infrastructure.UnitOfWork.UnitOfWorkSetup;

public class UnitOfWorks : IUnitOfWorks
{
    private readonly ApplicationDbContext _context;
    private IProductRepository _productRepository;
    private ICategoryRepository _categoryRepository;

    public UnitOfWorks(ApplicationDbContext context, IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _context = context;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public IProductRepository ProductRepository => _productRepository;
    public ICategoryRepository CategoryRepository => _categoryRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 