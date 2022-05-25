using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Logger.Controllers
{
    public abstract class LoggerControllerBase: AbpController
    {
        protected LoggerControllerBase()
        {
            LocalizationSourceName = LoggerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
