﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Data
{
    /// <summary>
    ///
    /// </summary>
    public static class PreSeeder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
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

            // seed currency
            if (!context.Currencies.Any())
            {
                var request = await CurrencyRate.GetExchangeRate();
                var rates = ReflectionConverter.GetPropertyValues(request.Rates);

                for (int i = 0; i < rates.Count; i++)
                {
                    var name = ReflectionConverter.GetPropertyName(rates[i]);
                    var value = ReflectionConverter.GetPropertyValue(request.Rates, name);

                    context.Currencies.Add(new Currency { Code = name });
                    await context.SaveChangesAsync();
                }
            }

            // pre-load Admin user
            if (!userManager.Users.Any())
            {
                User user = new User()
                {
                    FirstName = "Victor",
                    LastName = "Nwike",
                    UserName = "ugo@gmail.com",
                    Email = "ugo@gmail.com",
                    PhoneNumber = "08165585587",
                    Address = "Sangotedo"
                };

                var result = await userManager.CreateAsync(user, "12345Admin");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}