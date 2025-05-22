//using EShop.OrderService.Application.Common.Interfaces;
//using EShop.OrderService.Infrastructure.Persistence;
//using EShop.OrderService.Infrastructure.Repository;
//using EShop.OrderService.Infrastructure.UnitOfWork;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace EShop.OrderService.Infrastructure;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddDbContext<ApplicationDbContext>(options =>
//            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
//                npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()));

//        services.AddHttpClient();
        
//        services.AddScoped<IOrderRepository, OrderRepository>();
//        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
//        services.AddScoped<IProductRepository, ProductRepository>();
//        services.AddScoped<IUnitOfWork, UnitOfWork>();

//        return services;
//    }
//} 