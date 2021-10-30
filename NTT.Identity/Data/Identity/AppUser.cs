using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NTT.Identity.Data.Identity
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }
    }
}
