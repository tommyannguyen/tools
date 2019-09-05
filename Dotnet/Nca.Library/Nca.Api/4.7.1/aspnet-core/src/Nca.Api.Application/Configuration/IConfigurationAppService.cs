using System.Threading.Tasks;
using Nca.Api.Configuration.Dto;

namespace Nca.Api.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
