using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using Logger.Project.Dto;

namespace Logger.LogEntry.Dto
{
    [AutoMapFrom(typeof(LogEntry))]
    public class LogEntryDto : EntityDto
    {
        public string Log { get; set; }
        public string Severity { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProjectId { get; set; }
        public ProjectDto Project { get; set; }


        public string getSeverityColorClass
        {
            get
            {
                if (Severity == SeverityEnum.emerg.ToString() || Severity == SeverityEnum.alert.ToString() || Severity == SeverityEnum.crit.ToString() || Severity == SeverityEnum.err.ToString() )
                {
                    return "badge badge-danger";
                }
                else if (Severity == SeverityEnum.warning.ToString())
                {
                    return "badge badge-warning";
                }
                else if (Severity == SeverityEnum.notice.ToString() || Severity == SeverityEnum.info.ToString())
                {
                    return "badge badge-info";
                }

                return "badge badge-secondary";
            }
        }
    }
}
