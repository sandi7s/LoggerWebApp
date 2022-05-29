using Logger.LogEntry;
using Logger.LogEntry.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Logger.Tests.LogEntryes
{
    public class LogEntryes_Tests : LoggerTestBase
    {
        private readonly ILogEntryAppService _logEntryAppService;

        public LogEntryes_Tests()
        {
            _logEntryAppService = Resolve<ILogEntryAppService>();
        }

        [Fact]
        public async Task GetLogEntrys_Test()
        {
            await _logEntryAppService.CreateAsync(
                new LogEntry.Dto.CreateLogEntryDto
                {
                    Log = "LogEntry 1yxcvbnm",
                    Severity = "www.logEntry1.com",
                    TimeStamp = DateTime.UtcNow,
                    ProjectId = 0
                });

            var output = await _logEntryAppService.GetAllAsync(new PagedLogEntryResultRequestDto { MaxResultCount = 20, SkipCount = 0 });

            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CRUDLogEntry_Test()
        {
            // Act
            await _logEntryAppService.CreateAsync(
                new LogEntry.Dto.CreateLogEntryDto
                {
                    Log = "LogEntry 1yxcvbnm",
                    Severity = "www.logEntry1.com",
                    TimeStamp = DateTime.UtcNow,
                    ProjectId = 0
                });

            int id = 1;

            await UsingDbContextAsync(async context =>
            {
                var newLogEntry = context.LogEntry.Where(e => e.Log == "LogEntry 1yxcvbnm").FirstOrDefault();
                newLogEntry.ShouldNotBeNull();
                id = newLogEntry.Id;
            });

            var logEntry = await _logEntryAppService.GetAsync(
                new LogEntry.Dto.LogEntryDto
                {
                    Id = id,
                });

            logEntry.ShouldNotBeNull();

            logEntry.Log = "updated 1yxcvbnm";

            await _logEntryAppService.UpdateAsync(logEntry);

            await UsingDbContextAsync(async context =>
            {
                var updatedLogEntry = context.LogEntry.Where(e => e.Log == "updated 1yxcvbnm").FirstOrDefault();
                updatedLogEntry.ShouldNotBeNull();
                id = updatedLogEntry.Id;

                await _logEntryAppService.DeleteAsync(
                new LogEntry.Dto.LogEntryDto
                {
                    Id = id,
                });
            });

            await UsingDbContextAsync(async context =>
            {
                var deletedLogEntry = context.LogEntry.Where(e => e.Id == id).FirstOrDefault();

                deletedLogEntry.IsDeleted.ShouldBe(true);
            });
        }
    }
}
