using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FinanceManagerBack.Data.Initializer
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "sukhyiroman@gmail.com";
            string password = "pass-1234";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("consultant") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("consultant"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    admin.EmailConfirmed = true;
                    admin.Name = "Roman";
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}