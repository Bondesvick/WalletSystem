using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Data
{
    public static class PreSeeder
    {
        public static async Task Seed(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            // pre-load data to roles table
            await context.Database.EnsureCreatedAsync();

            if (!roleManager.Roles.Any())
            {
                var listOfRoles = new List<IdentityRole>
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("Elite"),
                    new IdentityRole("Noob")
                };
                foreach (var role in listOfRoles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // pre-load Admin user
            if (!userManager.Users.Any())
            {
            }
        }
    }
}