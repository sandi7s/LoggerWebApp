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


        public string getSeverityColorClass
        {
            get
            {
                if (Severity == "emerg" || Severity == "alert" || Severity == "crit" || Severity == "err" || Severity == "error")
                {
                    return "badge badge-danger";
                }
                else if (Severity == "warning")
                {
                    return "badge badge-warning";
                }
                else if (Severity == "notice" || Severity == "info")
                {
                    return "badge badge-info";
                }

                return "badge badge-secondary";
            }
        }
    }
}
