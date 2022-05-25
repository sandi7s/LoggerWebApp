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
using Logger.Project.Dto;
using Microsoft.AspNetCore.Identity;

namespace Logger.Project
{
    [AbpAuthorize(PermissionNames.Pages_Projects)]
    public class ProjectAppService : AsyncCrudAppService<Project, ProjectDto, int, PagedProjectResultRequestDto, CreateProjectDto, ProjectDto>, IProjectAppService
    {
        //private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        public ProjectAppService(
            IRepository<Project, int> repository
            //IAbpZeroDbMigrator abpZeroDbMigrator
            )
            : base(repository)
        {
            
        }
    }
}

