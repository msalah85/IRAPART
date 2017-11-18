using System.Threading.Tasks;
using IRACMS.Configuration.Dto;

namespace IRACMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}