using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using NTT.Identity.Data.Identity;

namespace NTT.Identity.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IClientStore clientStore, IEventService events)
        {
            _userManager = userManager;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
            _events = events;
            _signInManager = signInManager;
        }

        //[HttpPost]
        //[Route("api/[controller]")]
        //public async Task<IActionResult> Register([FromBody] RegisterRequestViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = new AppUser { UserName = model.Email, Name = model.Name, Email = model.Email };

        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (!result.Succeeded) return BadRequest(result.Errors);

        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.Name));
        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
        //    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", Roles.Consumer));

        //    return Ok(new RegisterResponseViewModel(user));
        //}
    }
}
