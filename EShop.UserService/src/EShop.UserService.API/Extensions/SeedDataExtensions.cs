using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.Configurations;
using EShop.UserService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.UserService.API.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            // Apply migrations
            var dbContext = services.GetRequiredService<UserDbContext>();
            await dbContext.Database.MigrateAsync();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            await RoleConfiguration.SeedRolesAsync(roleManager);
            await UserConfigurations.SeedUsersAsync(userManager, roleManager);
        }
    }
}
