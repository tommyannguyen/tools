using System.Threading.Tasks;
using Abp.Application.Services;
using Nca.Api.Sessions.Dto;

namespace Nca.Api.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
