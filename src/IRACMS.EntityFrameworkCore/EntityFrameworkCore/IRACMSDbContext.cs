using Abp.Zero.EntityFrameworkCore;
using IRACMS.Authorization.Roles;
using IRACMS.Authorization.Users;
using IRACMS.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace IRACMS.EntityFrameworkCore
{
    public class IRACMSDbContext : AbpZeroDbContext<Tenant, Role, User, IRACMSDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        
        public IRACMSDbContext(DbContextOptions<IRACMSDbContext> options)
            : base(options)
        {

        }
    }
}
