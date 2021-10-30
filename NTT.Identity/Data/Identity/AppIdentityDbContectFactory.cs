using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NTT.Identity.Data.AuthServer.Infrastructure.Data;

namespace NTT.Identity.Data.Identity
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
        {
            return new AppIdentityDbContext(options);
        }
    }
}
