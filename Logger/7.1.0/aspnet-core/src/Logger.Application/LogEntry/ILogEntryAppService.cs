using Abp.Application.Services;
using Logger.MultiTenancy.Dto;
using Logger.LogEntry.Dto;

namespace Logger.LogEntry
{
    public interface ILogEntryAppService : IAsyncCrudAppService<LogEntryDto, int, PagedLogEntryResultRequestDto, CreateLogEntryDto, LogEntryDto>
    {
    }
}

