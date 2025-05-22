using EShop.ProductService.Application.Common.Interfaces;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Infrastructure.Repository.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task Add(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        product.IsDeleted = true;
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id && !p.IsDeleted);
    }
} 