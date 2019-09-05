using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Nca.Api.MultiTenancy.Dto;

namespace Nca.Api.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

