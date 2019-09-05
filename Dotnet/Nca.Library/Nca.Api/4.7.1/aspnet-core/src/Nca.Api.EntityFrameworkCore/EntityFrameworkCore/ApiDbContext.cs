using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Nca.Api.Authorization.Roles;
using Nca.Api.Authorization.Users;
using Nca.Api.MultiTenancy;

namespace Nca.Api.EntityFrameworkCore
{
    public class ApiDbContext : AbpZeroDbContext<Tenant, Role, User, ApiDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
    }
}
