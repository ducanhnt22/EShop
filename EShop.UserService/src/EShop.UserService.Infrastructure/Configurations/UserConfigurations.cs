using EShop.UserService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.Configurations
{
    public static class UserConfigurations
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            string email = "admin@gmail.com";
            string password = "Password@123";

            var role = await roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                role = new IdentityRole<Guid> { Name = "Admin" };
                await roleManager.CreateAsync(role);
            }

            var admin = await userManager.FindByEmailAsync(email);
            if (admin == null)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Admin",
                    PhoneNumber = "1234567890",
                    Email = email,
                    UserName = email,
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
