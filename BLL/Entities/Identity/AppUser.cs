using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BLL.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
        }

        public string DisplayName { get; set; }

        public Address Address { get; set; }

       
    }
}
