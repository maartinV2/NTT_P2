using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NTT.API.Models.Dto;
using NTT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NTT.Identity.Areas.Identity.Data;
using NTT.Identity.Controller;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTT.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString;

        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

        }



        [HttpGet("{userId}")]
        public ActionResult<UserDto> GetById(string userId)
        {
            using var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetById(userId);
            var userDto = new UserDto().FromDomain(user);
            return userDto;
        }



    }
}
