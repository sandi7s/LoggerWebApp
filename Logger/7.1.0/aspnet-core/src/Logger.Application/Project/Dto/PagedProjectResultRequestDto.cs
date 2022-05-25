using Abp.Application.Services.Dto;

namespace Logger.Project.Dto
{
    public class PagedProjectResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        //public bool? IsActive { get; set; }
    }
}

