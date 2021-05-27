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

        public List<Address> Address { get; set; }

       
    }
}
