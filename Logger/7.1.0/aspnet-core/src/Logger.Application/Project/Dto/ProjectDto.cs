using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace Logger.Project.Dto
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
