using EShop.ProductService.Application.Common.Interfaces;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Infrastructure.Persistence;
using EShop.ProductService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Infrastructure.Repository.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
} 