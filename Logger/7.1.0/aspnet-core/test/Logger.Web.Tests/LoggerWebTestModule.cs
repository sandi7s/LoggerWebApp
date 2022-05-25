using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Logger.EntityFrameworkCore;
using Logger.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Logger.Web.Tests
{
    [DependsOn(
        typeof(LoggerWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LoggerWebTestModule : AbpModule
    {
        public LoggerWebTestModule(LoggerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LoggerWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LoggerWebMvcModule).Assembly);
        }
    }
}