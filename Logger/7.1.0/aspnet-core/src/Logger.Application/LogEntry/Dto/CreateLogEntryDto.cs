using System;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace Logger.LogEntry.Dto
{
    [AutoMapTo(typeof(LogEntry))]
    public class CreateLogEntryDto
    {
        public string Log { get; set; }
        public string Severity { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProjectId { get; set; }
    }
}
