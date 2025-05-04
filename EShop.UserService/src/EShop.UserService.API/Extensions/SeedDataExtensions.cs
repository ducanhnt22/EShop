using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;

namespace EShop.UserService.API.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            await RoleConfiguration.SeedRolesAsync(roleManager);
            await UserConfigurations.SeedUsersAsync(userManager, roleManager);
        }
    }
}
