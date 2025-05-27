using EShop.VendorService.API.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.VendorService.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AspireEShopVendorDB")));
    }
}
