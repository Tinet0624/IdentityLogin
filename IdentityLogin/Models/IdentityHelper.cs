using Microsoft.AspNetCore.Identity;

#nullable disable

namespace IdentityLogin.Models
{
    public static class IdentityHelper
    {
        //Keeps track of roles
        public const string Instructor = "Instructor";
        public const string Student = "Student";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExsist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExsist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // if no users are present make default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count;
             if (numUsers == 0) // if no users are in the specified role
            {
                var defaultUser = new IdentityUser()
                {
                    Email = "instructor@IdentityHelper.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "Password");

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
