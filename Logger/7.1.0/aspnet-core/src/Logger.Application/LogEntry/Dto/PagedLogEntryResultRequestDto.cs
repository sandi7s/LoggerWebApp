﻿using Abp.Application.Services.Dto;

namespace Logger.LogEntry.Dto
{
    public class PagedLogEntryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        //public bool? IsActive { get; set; }
    }
}

