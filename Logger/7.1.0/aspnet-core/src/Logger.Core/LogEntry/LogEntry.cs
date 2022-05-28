using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.LogEntry
{
    public class LogEntry : FullAuditedEntity
    {
        public string Log { get; set; }
        public string Severity { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project.Project Project { get; set; }
        
    }
}
