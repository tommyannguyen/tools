using System.Threading.Tasks;
using Abp.Application.Services;
using Nca.Api.Authorization.Accounts.Dto;

namespace Nca.Api.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
