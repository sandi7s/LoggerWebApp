using Abp.Application.Services;
using Logger.MultiTenancy.Dto;
using Logger.LogEntry.Dto;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Logger.LogEntry
{
    public interface ILogEntryAppService : IAsyncCrudAppService<LogEntryDto, int, PagedLogEntryResultRequestDto, CreateLogEntryDto, LogEntryDto>
    {
        Task<PagedResultDto<LogEntryDto>> GetAllPagedAndFiltered(PagedLogEntryResultRequestDto input);
        //Task<LogStats> GetStatsLastDay(int? projectId);
        //Task<LogStats> GetStatsLastHour(int? projectId);
        //Task<List<LogStats>> GetStatsForEachSeverity(int? projectId);
        Task<List<LogStats>> GetStatsAllStats(int? projectId);
        Task<byte[]> CreateExcelLogs(PagedLogEntryResultRequestDto input);
    }
}

