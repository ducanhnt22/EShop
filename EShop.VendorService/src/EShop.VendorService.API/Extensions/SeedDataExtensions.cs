using EShop.VendorService.API.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.VendorService.API.Extensions;

public static class SeedDataExtensions
{
    public static async Task SeedDatabaseAsync(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        var dbContext = services.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
