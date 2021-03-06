﻿using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Nca.Api.Authorization;

namespace Nca.Api
{
    [DependsOn(
        typeof(ApiCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ApiApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ApiAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ApiApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
