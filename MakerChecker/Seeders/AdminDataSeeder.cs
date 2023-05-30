using Microsoft.AspNetCore.Identity;

namespace MakerChecker.Seeders;

public static class AdminDataSeeder
{
    public static async Task SeedAdminData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string roleName = "SuperAdmin";
            IdentityResult roleResult;
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

            var email = "superadmin@email.com";
            var password = "P@ssw0rd";
            var userExist = await userManager.FindByEmailAsync(email);
            if (userExist == null)
            {
                IdentityUser user = new()
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                };
                IdentityResult userResult = userManager.CreateAsync(user, password).Result;

                if (userResult.Succeeded)
                    userManager.AddToRoleAsync(user, roleName).Wait();
            }
        }
    }
}
