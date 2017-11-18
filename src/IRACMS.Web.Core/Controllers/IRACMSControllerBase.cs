using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace IRACMS.Controllers
{
    public abstract class IRACMSControllerBase: AbpController
    {
        protected IRACMSControllerBase()
        {
            LocalizationSourceName = IRACMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}