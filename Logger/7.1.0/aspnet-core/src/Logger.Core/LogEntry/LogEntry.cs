using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.LogEntry
{
    public class LogEntry : Entity
    {
        public string Log { get; set; }
        public string Severity { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProjectId { get; set; }
        
    }
}
