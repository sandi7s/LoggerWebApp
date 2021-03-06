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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Logger.LogEntry
{
    [AbpAuthorize(PermissionNames.Pages_LogEntryes)]
    public class LogEntryAppService : AsyncCrudAppService<LogEntry, LogEntryDto, int, PagedLogEntryResultRequestDto, CreateLogEntryDto, LogEntryDto>, ILogEntryAppService
    {

        public LogEntryAppService(
            IRepository<LogEntry, int> repository
            )
            : base(repository)
        {
            
        }

        public async Task<PagedResultDto<LogEntryDto>> GetAllPagedAndFiltered(PagedLogEntryResultRequestDto input)
        {
            input.Sorting = input.Sorting.ToLower();
            Func<LogEntry, Object> orderByFunc = null;
            if (input.Sorting.Contains("id"))
            {
                orderByFunc = item => item.Id;
            }
            else if (input.Sorting.Contains("timestamp"))
            {
                orderByFunc = item => item.TimeStamp;
            }

            //var prop = typeof(LogEntry).GetProperty(input.Sorting);
            //var user = AbpSession.UserId;
            var logs = Repository.GetAll().Include(e => e.Project)
                .WhereIf(!string.IsNullOrEmpty(input.Keyword), e => e.Log.Contains(input.Keyword))
                .WhereIf(input.ProjectId != null, e => e.ProjectId == input.ProjectId)
                .Where( e => e.CreatorUserId == AbpSession.UserId)
                //.OrderBy(e => prop.GetValue(e, null))
                //.OrderByDescending(e => e.TimeStamp)
                .ToList();

            if (input.Sorting.Contains("desc"))
            {
                logs = logs.OrderByDescending(orderByFunc).ToList();
            }
            else
            {
                logs = logs.OrderBy(orderByFunc).ToList();
            }

            var logsDtos = ObjectMapper.Map<List<LogEntryDto>>(logs);

            var paged = logsDtos.AsQueryable().PageBy(input.SkipCount, input.MaxResultCount).ToList();

            return new PagedResultDto<LogEntryDto>(
                    logs.Count(),
                    paged
                );
        }

        private async Task<LogStats> GetStatsLastDay(int? projectId)
        {
            var logs = Repository.GetAll()
                .Where(e => e.TimeStamp > DateTime.UtcNow.AddDays(-1))
                .WhereIf(projectId != null, e => e.ProjectId == projectId)
                .Where(e => e.CreatorUserId == AbpSession.UserId)
                .ToList();

            return new LogStats("Last 24h", logs.Count, "bg-success");
        }
        private async Task<LogStats> GetStatsLastHour(int? projectId)
        {
            var logs = Repository.GetAll()
                .Where(e => e.TimeStamp > DateTime.UtcNow.AddHours(-1))
                .WhereIf(projectId != null, e => e.ProjectId == projectId)
                .Where(e => e.CreatorUserId == AbpSession.UserId)
                .ToList();

            return new LogStats("Last hour", logs.Count, "bg-success");
        }

        private async Task<List<LogStats>> GetStatsForEachSeverity(int? projectId)
        {
            var severityes = Enum.GetValues(typeof(SeverityEnum))
                .Cast<SeverityEnum>()
                .Select(severityEnum => new LogStats
                {
                    DisplayText = severityEnum.ToString(),
                    Count = 0,
                    ColorClass = GetSeverityColorForStats(severityEnum.ToString())
                }).ToList();

            foreach (var item in severityes)
            {
                var logs = Repository.GetAll()
                .WhereIf(projectId != null, e => e.ProjectId == projectId)
                .Where(e => e.Severity == item.DisplayText)
                .Where(e => e.CreatorUserId == AbpSession.UserId)
                .ToList();

                item.Count = logs.Count;
            }

            return severityes;
        }

        public async Task<List<LogStats>> GetStatsAllStats(int? projectId)
        {
            List<LogStats> statList = new List<LogStats>();

            statList.Add(await GetStatsLastDay(projectId));
            statList.Add(await GetStatsLastHour(projectId));
            statList.AddRange(await GetStatsForEachSeverity(projectId));

            return statList;
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

        public async Task<byte[]> CreateExcelLogs(PagedLogEntryResultRequestDto input)
        {
            input.Keyword = null;
            input.SkipCount = 0;
            input.MaxResultCount = int.MaxValue;

            var pagedData = await GetAllPagedAndFiltered(input);

            var memoryStream = new MemoryStream();
            //var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var workbook = new XSSFWorkbook();
            var excelSheet = workbook.CreateSheet(pagedData.Items[0].Project.Name);

            var headerStyle = workbook.CreateCellStyle();
            var font = workbook.CreateFont();
            font.IsBold = true;
            headerStyle.SetFont(font);

            var headerRow = excelSheet.CreateRow(0);
            headerRow.RowStyle = headerStyle;

            var cell1 = headerRow.CreateCell(0);
            cell1.SetCellValue("Log");
            cell1.CellStyle = headerStyle;

            var cell2 = headerRow.CreateCell(1);
            cell2.SetCellValue("Severity");
            cell2.CellStyle = headerStyle;

            var cell3 = headerRow.CreateCell(2);
            cell3.SetCellValue("Timestamp");
            cell3.CellStyle = headerStyle;
            //headerRow.CreateCell(3).SetCellValue("Project");

            for (int i = 0; i < pagedData.Items.Count; i++)
            {
                var row = excelSheet.CreateRow(i+1);

                row.CreateCell(0).SetCellValue(pagedData.Items[i].Log);
                row.CreateCell(1).SetCellValue(pagedData.Items[i].Severity);
                row.CreateCell(2).SetCellValue(pagedData.Items[i].TimeStamp.ToString("dd/MM/yyyy HH:mm:ss"));
                //row.CreateCell(3).SetCellValue(pagedData.Items[i].Project.Name);
            }

            workbook.Write(memoryStream);

            return memoryStream.ToArray();
        }
    }
}

