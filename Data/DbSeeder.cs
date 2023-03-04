using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WEBGROUP_GCC0903.Constants;
using WEBGROUP_GCC0903.Models;

namespace WEBGROUP_GCC0903.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<WebApp1User>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // creating admin

            var user = new WebApp1User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Name = "Lucky Nguyen",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            var owner = new WebApp1User
            {
                UserName = "owner",
                Email = "owner@gmail.com",
                Name = "Owner",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var ownerInDb = await userManager.FindByEmailAsync(owner.Email);
            if (ownerInDb == null)
            {
                await userManager.CreateAsync(owner, "Owner@123");
                await userManager.AddToRoleAsync(owner, Roles.User.ToString());
            }
        }
    }
    
}
