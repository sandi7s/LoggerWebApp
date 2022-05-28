using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Project
{
    public class Project : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public bool ShowOnFrontPage { get; set; }
    }
}
