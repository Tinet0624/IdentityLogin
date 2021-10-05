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
    }
}
