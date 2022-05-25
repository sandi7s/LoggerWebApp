using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Logger.Authorization;

namespace Logger
{
    [DependsOn(
        typeof(LoggerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LoggerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LoggerAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(LoggerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
