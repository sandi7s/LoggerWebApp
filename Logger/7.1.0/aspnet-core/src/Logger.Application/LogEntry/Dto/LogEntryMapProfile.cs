using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.LogEntry.Dto
{
    public class LogEntryMapProfile : Profile
    {
        public LogEntryMapProfile()
        {
            CreateMap<LogEntryDto, LogEntry>();
            //CreateMap<LogEntryDto, LogEntry>()
            //    .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<LogEntryDto, LogEntry>();
        }
    }
}
