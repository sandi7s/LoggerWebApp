﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Logger.Configuration.Dto;

namespace Logger.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LoggerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
