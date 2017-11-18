using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using IRACMS.Configuration.Dto;

namespace IRACMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IRACMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
