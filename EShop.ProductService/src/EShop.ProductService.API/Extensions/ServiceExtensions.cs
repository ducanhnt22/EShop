using EShop.ProductService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Add database connection pooling with performance optimizations
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("AspireEShopProductDB"), npgsqlOptions =>
                {
                    // Enable connection pooling
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                });
                
                // Performance optimizations
                options.EnableSensitiveDataLogging(false);
                options.EnableServiceProviderCaching(true);
                options.EnableDetailedErrors(false);
                options.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            }, poolSize: 128); // Optimize pool size based on load
        }
    }
}
