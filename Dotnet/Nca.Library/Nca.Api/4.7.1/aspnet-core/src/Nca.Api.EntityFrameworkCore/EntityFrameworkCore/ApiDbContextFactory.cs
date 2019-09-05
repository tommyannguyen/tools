using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Nca.Api.Configuration;
using Nca.Api.Web;

namespace Nca.Api.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApiDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ApiDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ApiConsts.ConnectionStringName));

            return new ApiDbContext(builder.Options);
        }
    }
}
