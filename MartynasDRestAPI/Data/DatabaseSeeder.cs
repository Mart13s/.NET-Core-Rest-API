using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<RestUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DatabaseSeeder(UserManager<RestUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            foreach (var role in RestUserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

                var newAdminUser = new RestUser()
                {
                    UserName = "Admin",
                    Email = "admin@admin.com"

                };

                var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
                if(existingAdminUser == null)
                {
                   var adminUserResult = await _userManager.CreateAsync(newAdminUser, "SomePassword1!");
                   if(adminUserResult != null)
                   {
                        await _userManager.AddToRolesAsync(newAdminUser, RestUserRoles.All);
                   }
                }
            
        }
    }
}
