using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Nca.Library.Repositories.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=IdentitySample.sqlite;", option => {
                option.MigrationsAssembly("Nca.Web.Spa");
            });
        }
    }

}
