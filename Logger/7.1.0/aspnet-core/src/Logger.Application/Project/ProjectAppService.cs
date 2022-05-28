using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
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
using Logger.LogEntry.Dto;
using Logger.MultiTenancy.Dto;
using Logger.Project.Dto;
using Microsoft.AspNetCore.Identity;

namespace Logger.Project
{
    [AbpAuthorize(PermissionNames.Pages_Projects)]
    public class ProjectAppService : AsyncCrudAppService<Project, ProjectDto, int, PagedProjectResultRequestDto, CreateProjectDto, ProjectDto>, IProjectAppService
    {
        private readonly IRepository<LogEntry.LogEntry, int> _logEntryRepository;

        public ProjectAppService(
            IRepository<Project, int> repository,
            IRepository<LogEntry.LogEntry, int> logEntryRepository
            //IAbpZeroDbMigrator abpZeroDbMigrator
            )
            : base(repository)
        {
            _logEntryRepository = logEntryRepository;
        }


        public async Task<PagedResultDto<ProjectDto>> GetAllPagedAndFiltered(PagedProjectResultRequestDto input)
        {
            var projects = Repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Keyword), e => e.Name.Contains(input.Keyword) )
                .Where(e => e.CreatorUserId == AbpSession.UserId)
                .ToList();

            var projectsDtos = ObjectMapper.Map<List<ProjectDto>>(projects);

            var paged = projectsDtos.AsQueryable().PageBy(input.SkipCount, input.MaxResultCount).ToList();

            return new PagedResultDto<ProjectDto>(
                    projects.Count(),
                    paged
                );
        }

        public async Task<List<ProjectDto>> GetAllForFrontPage()
        {
            var projects = Repository.GetAll()
                .Where(e => e.CreatorUserId == AbpSession.UserId)
                .ToList();

            var projectsDtos = ObjectMapper.Map<List<ProjectDto>>(projects);

            foreach (var item in projectsDtos)
            {
                var lastLog = _logEntryRepository.GetAll().Where(e => e.ProjectId == item.Id && e.CreatorUserId == AbpSession.UserId).OrderByDescending(e => e.TimeStamp).FirstOrDefault();

                if (lastLog != null)
                {
                    item.LastLogEntryColor = GetSeverityColorForStats(lastLog.Severity.ToString());
                }
                else
                {
                    item.LastLogEntryColor = "bg-secondary";
                }
            }

            return projectsDtos;
        }
        private string GetSeverityColorForStats(string severity)
        {
            if (severity == SeverityEnum.emerg.ToString() || severity == SeverityEnum.alert.ToString() || severity == SeverityEnum.crit.ToString() || severity == SeverityEnum.err.ToString())
            {
                return "bg-danger";
            }
            else if (severity == SeverityEnum.warning.ToString())
            {
                return "bg-warning";
            }
            else if (severity == SeverityEnum.notice.ToString() || severity == SeverityEnum.info.ToString())
            {
                return "bg-info";
            }

            return "bg-secondary";
        }
    }
}

