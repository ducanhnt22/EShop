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

    public UnitOfWorks(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            _productRepository ??= new ProductRepository(_context);
            return _productRepository;
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            _categoryRepository ??= new CategoryRepository(_context);
            return _categoryRepository;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
} 