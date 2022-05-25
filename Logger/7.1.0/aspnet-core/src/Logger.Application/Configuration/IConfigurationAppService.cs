using System.Threading.Tasks;
using Logger.Configuration.Dto;

namespace Logger.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
