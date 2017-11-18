using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IRACMS.Authorization;

namespace IRACMS
{
    [DependsOn(
        typeof(IRACMSCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class IRACMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<IRACMSAuthorizationProvider>();
        }

        public override void Initialize()
        {
            Assembly thisAssembly = typeof(IRACMSApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}