using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace Logger.Project.Dto
{
    [AutoMapTo(typeof(Project))]
    public class CreateProjectDto
    {

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
