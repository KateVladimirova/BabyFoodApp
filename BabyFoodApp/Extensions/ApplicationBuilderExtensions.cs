using BabyFoodApp.BabyFoodCommons;
using BabyFoodApp.Data.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Construction;

namespace BabyFoodApp.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdministrator(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync("Administrator"))
                {
                    return;
                }

                var role = new IdentityRole();
                role.Name = "Administrator";

                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync(Constants.AdminEmail);
                await userManager.AddToRoleAsync(admin, role.Name);

            })
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
