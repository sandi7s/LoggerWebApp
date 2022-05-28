using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Logger.MultiTenancy.Dto;
using Logger.Project.Dto;
using System.Threading.Tasks;

namespace Logger.Project
{
    public interface IProjectAppService : IAsyncCrudAppService<ProjectDto, int, PagedProjectResultRequestDto, CreateProjectDto, ProjectDto>
    {
        Task<PagedResultDto<ProjectDto>> GetAllPagedAndFiltered(PagedProjectResultRequestDto input);
    }
}

