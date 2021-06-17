using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace BLL.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public AppIdentityDbContextSeed()
        {
        }

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
              var addresses =  new Address
                {
                    FirstName = "Sammy",
                    LastName = "SamT",
                    Street = "10 bar",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10218"
                };
                var user = new AppUser
                {
                    DisplayName = "Sammy",
                    Email = "sammy@mail.com",
                    UserName = "sammy@mail.com",
                    Address = addresses
                };

                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        }

    }
}
