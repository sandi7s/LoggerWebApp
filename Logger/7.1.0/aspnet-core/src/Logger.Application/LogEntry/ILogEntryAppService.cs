using Abp.Application.Services;
using Logger.MultiTenancy.Dto;
using Logger.LogEntry.Dto;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;

namespace Logger.LogEntry
{
    public interface ILogEntryAppService : IAsyncCrudAppService<LogEntryDto, int, PagedLogEntryResultRequestDto, CreateLogEntryDto, LogEntryDto>
    {
        Task<PagedResultDto<LogEntryDto>> GetAllPagedAndFiltered(PagedLogEntryResultRequestDto input);
    }
}

