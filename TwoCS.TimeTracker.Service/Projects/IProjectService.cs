namespace TwoCS.TimeTracker.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Repositories;
    using TwoCS.TimeTracker.Core.Services;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Mapper;

    public interface IProjectService : IService<Project>
    {
        Task<ProjectDto> CreateAsync(AddProjectDto dto);
        Task<IEnumerable<ProjectDto>> SearchAsync();

        Task<ProjectDto> GetDetailAsync(string name);
    }


    public class ProjectService : ServiceBase<Project>, IProjectService
    {
        public ProjectService(IProjectRepository repositoty)
            : base(repositoty)
        {

        }

        public async Task<ProjectDto> CreateAsync(AddProjectDto dto)
        {
            var existedProject = await Repository.SingleAsync(s => s.Name == dto.Name);

            if (existedProject != null)
            {
                throw new BadRequestException("User is ready existed.");
            }

            var entity = dto.ToEntity();

            var result = await CreateAsync(entity);

            return result?.ToDto();
        }

        public async Task<ProjectDto> GetDetailAsync(string name)
        {
            var entity = await Repository.SingleAsync(s => s.Name == name);

            return entity?.ToDto();
        }

        public async Task<IEnumerable<ProjectDto>> SearchAsync()
        {
            var result = await ReadAllAsync();

            var projects = result?.Select((s, idx) => s.ToDto())
                .ToList();

            projects.ForEach(s => {
                s.OrderNo = projects.IndexOf(s) + 1;
            });

            return projects;
        }
    }
}
