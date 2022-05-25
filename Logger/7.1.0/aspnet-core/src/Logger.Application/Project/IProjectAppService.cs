using Abp.Application.Services;
using Logger.MultiTenancy.Dto;
using Logger.Project.Dto;

namespace Logger.Project
{
    public interface IProjectAppService : IAsyncCrudAppService<ProjectDto, int, PagedProjectResultRequestDto, CreateProjectDto, ProjectDto>
    {
    }
}

