using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Project.Dto
{
    public class ProjectMapProfile : Profile
    {
        public ProjectMapProfile()
        {
            CreateMap<ProjectDto, Project>();
            //CreateMap<ProjectDto, Project>()
            //    .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<ProjectDto, Project>();
        }
    }
}
