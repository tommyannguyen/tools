using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Nca.Api.Configuration.Dto;

namespace Nca.Api.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ApiAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
