using EShop.ProductService.Application.Common.Interfaces;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Infrastructure.Persistence;
using EShop.ProductService.Infrastructure.Repository;
using EShop.ProductService.Infrastructure.Repository.Repositories;
using EShop.ProductService.Infrastructure.UnitOfWork;
using EShop.ProductService.Infrastructure.UnitOfWork.UnitOfWorkSetup;
using EShop.ProductService.Infrastructure.Cachings.CachingService;
using EShop.ProductService.Infrastructure.Cachings.ICachingService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.ProductService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWorks, UnitOfWorks>();
        
        // Add caching services
        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
} 