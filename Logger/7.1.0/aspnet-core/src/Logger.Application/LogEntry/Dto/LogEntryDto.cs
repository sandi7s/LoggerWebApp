using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace Logger.LogEntry.Dto
{
    [AutoMapFrom(typeof(LogEntry))]
    public class LogEntryDto : EntityDto
    {
        public string Log { get; set; }
        public string Severity { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProjectId { get; set; }
    }
}
