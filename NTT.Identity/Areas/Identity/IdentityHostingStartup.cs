﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTT.Identity.Areas.Identity.Data;

[assembly: HostingStartup(typeof(NTT.Identity.Areas.Identity.IdentityHostingStartup))]
namespace NTT.Identity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        //public void Configure(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices((context, services) => {
        //        services.AddDbContext<ApplicationDbContext>(options =>
        //            options.UseSqlServer(
        //                context.Configuration.GetConnectionString("DefaultConnection")));

        //       services.AddDefaultIdentity<AppUser>(options =>
        //           {
                       
        //               options.SignIn.RequireConfirmedAccount = true;
        //               //options.Password.RequireLowercase = false;
        //               //options.Password.RequireUppercase = false;
        //           })
        //           .AddEntityFrameworkStores<ApplicationDbContext>();
        //    });
        //}

       public void Configure(IWebHostBuilder builder)
       {
           builder.ConfigureServices((context, services) => {
           });
       }
    }
}