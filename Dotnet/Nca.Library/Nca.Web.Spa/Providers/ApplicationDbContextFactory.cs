using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Nca.Library.Repositories.Database;
using System.IO;

namespace Nca.Web.Spa.Providers
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite(@"Data Source=IdentitySample.sqlite;", option => {
                option.MigrationsAssembly("Nca.Web.Spa");
            });
            var dbContext = new ApplicationDbContext(builder.Options);
            dbContext.Database.Migrate();
            return dbContext;
        }
    }
}
