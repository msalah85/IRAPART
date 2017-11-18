using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IRACMS.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace IRACMS.Web.Host.Startup
{
    [DependsOn(
       typeof(IRACMSWebCoreModule))]
    public class IRACMSWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public IRACMSWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IRACMSWebHostModule).GetAssembly());
        }
    }
}
