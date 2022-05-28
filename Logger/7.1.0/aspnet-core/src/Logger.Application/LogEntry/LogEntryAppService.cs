using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Logger.Authorization;
using Logger.Authorization.Roles;
using Logger.Authorization.Users;
using Logger.Editions;
using Logger.MultiTenancy.Dto;
using Logger.LogEntry.Dto;
using Microsoft.AspNetCore.Identity;

namespace Logger.LogEntry
{
    [AbpAuthorize(PermissionNames.Pages_LogEntryes)]
    public class LogEntryAppService : AsyncCrudAppService<LogEntry, LogEntryDto, int, PagedLogEntryResultRequestDto, CreateLogEntryDto, LogEntryDto>, ILogEntryAppService
    {
        //private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        public LogEntryAppService(
            IRepository<LogEntry, int> repository
            //IAbpZeroDbMigrator abpZeroDbMigrator
            )
            : base(repository)
        {
            
        }
    }
}

