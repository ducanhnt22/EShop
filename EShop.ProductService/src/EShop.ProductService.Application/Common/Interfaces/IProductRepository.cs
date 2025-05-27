using EShop.ProductService.Application.Interfaces.Repositories;
using EShop.ProductService.Domain.Entities;

namespace EShop.ProductService.Application.Common.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
} 