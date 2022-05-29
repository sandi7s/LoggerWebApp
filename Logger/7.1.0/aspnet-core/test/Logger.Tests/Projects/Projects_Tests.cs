using Logger.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Logger.Project.Dto;

namespace Logger.Tests.Projects
{

    public class Projects_Tests : LoggerTestBase
    {
        private readonly IProjectAppService _projectAppService;

        public Projects_Tests()
        {
            _projectAppService = Resolve<IProjectAppService>();
        }

        [Fact]
        public async Task GetProjects_Test()
        {
            await _projectAppService.CreateAsync(
                new Project.Dto.CreateProjectDto
                {
                    Name = "Project 1yxcvbnm",
                    Url = "www.project1.com",
                    ShowOnFrontPage = true
                });

            var output = await _projectAppService.GetAllAsync(new PagedProjectResultRequestDto { MaxResultCount = 20, SkipCount = 0 });

            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CRUDProject_Test()
        {
            // Act
            await _projectAppService.CreateAsync(
                new Project.Dto.CreateProjectDto
                {
                    Name = "Project 1yxcvbnm",
                    Url = "www.project1.com",
                    ShowOnFrontPage = true
                });

            int id = 1;
            await UsingDbContextAsync(async context =>
            {
                var newProject = context.Project.Where(e => e.Name == "Project 1yxcvbnm").FirstOrDefault();
                newProject.ShouldNotBeNull();
                id = newProject.Id;
            });

            var project = await _projectAppService.GetAsync(
                new Project.Dto.ProjectDto
                {
                    Id = id,
                });

            project.ShouldNotBeNull();

            project.Name = "updated name 1yxcvbnm";

            await _projectAppService.UpdateAsync(project);

            await UsingDbContextAsync(async context =>
            {
                var updatedProject = context.Project.Where(e => e.Name == "updated name 1yxcvbnm").FirstOrDefault();
                updatedProject.ShouldNotBeNull();
                id = updatedProject.Id;

                await _projectAppService.DeleteAsync(
                new Project.Dto.ProjectDto
                {
                    Id = id,
                });
            });

            await UsingDbContextAsync(async context =>
            {
                var deletedProject = context.Project.Where(e => e.Id == id).FirstOrDefault();

                deletedProject.IsDeleted.ShouldBe(true);
            });
        }
    }
}
