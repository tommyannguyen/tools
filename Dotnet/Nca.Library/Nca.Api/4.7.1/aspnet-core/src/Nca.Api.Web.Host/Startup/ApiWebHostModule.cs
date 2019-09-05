using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Nca.Api.Configuration;

namespace Nca.Api.Web.Host.Startup
{
    [DependsOn(
       typeof(ApiWebCoreModule))]
    public class ApiWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ApiWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ApiWebHostModule).GetAssembly());
        }
    }
}
