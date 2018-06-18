namespace TwoCS.TimeTracker.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;

    public interface IProjectService : IService<Project>
    {
        Task<ProjectDto> CreateAsync(AddProjectDto dto);
        Task<IEnumerable<ProjectDto>> SearchAsync(string userName);

        Task<ProjectDto> GetDetailAsync(string projectName);
    }
}
