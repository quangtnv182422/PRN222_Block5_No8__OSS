using Microsoft.AspNetCore.Identity;
using OSS_Main.Models.Entity;

namespace OSS_Main.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAsync(RoleManager<AspNetRole> roleManager)
        {
            var roleNames = new[] { "Admin", "Sales", "Customer" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var role = new AspNetRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
