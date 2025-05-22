using EShop.ProductService.Application.Common.Interfaces;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Infrastructure.Repository.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category> GetById(Guid id)
    {
        return await _context.Categories
            .Include(c => c.Products.Where(p => !p.IsDeleted))
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories
            .Include(c => c.Products.Where(p => !p.IsDeleted))
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task Add(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public void Update(Category category)
    {
        category.UpdatedAt = DateTime.UtcNow;
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        category.IsDeleted = true;
        category.UpdatedAt = DateTime.UtcNow;
        _context.Categories.Update(category);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id && !c.IsDeleted);
    }
} 