using EShop.ProductService.Application.Common.Interfaces;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Infrastructure.Persistence;
using EShop.ProductService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Infrastructure.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
} 