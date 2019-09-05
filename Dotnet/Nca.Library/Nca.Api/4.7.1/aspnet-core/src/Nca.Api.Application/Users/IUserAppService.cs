using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Nca.Api.Roles.Dto;
using Nca.Api.Users.Dto;

namespace Nca.Api.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
