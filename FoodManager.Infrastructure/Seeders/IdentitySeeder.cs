using FoodManager.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace FoodManager.Infrastructure.Seeders
{
    public class IdentitySeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        public async Task Seed()
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            if (!await roleManager.RoleExistsAsync(Roles.User))
                await roleManager.CreateAsync(new IdentityRole(Roles.User));

            const string adminEmail = "admin@foodmanager.local";
            const string adminPassword = "Admin123!";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin is null)
            {
                admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createAdminResult = await userManager.CreateAsync(admin, adminPassword);

                if (!createAdminResult.Succeeded)
                {
                    var errors = string.Join(", ", createAdminResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create admin user: {errors}");
                }
            }

            if (!await userManager.IsInRoleAsync(admin, Roles.Admin))
                await userManager.AddToRoleAsync(admin, Roles.Admin);
        }
    }
}
