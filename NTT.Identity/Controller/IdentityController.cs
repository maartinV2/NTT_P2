using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NTT.Identity.Areas.Identity.Data;

namespace NTT.Identity.Controller
{
    public class IdentityController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityController()
        {}

        public IdentityController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string CurrentUserId()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return _userManager.GetUserId(User);
            }
            else
            {
                return "none";
            }
        }

    }
}
