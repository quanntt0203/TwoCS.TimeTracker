namespace TwoCS.TimeTracker.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Repositories;
    using TwoCS.TimeTracker.Core.Services;
    using TwoCS.TimeTracker.Core.Settings;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Mapper;

    public class ProjectService : ServiceBase<Project>, IProjectService
    {
        private readonly IUserRepository _userRepository;

        public ProjectService(IProjectRepository repositoty,
            IUserRepository userRepository)
            : base(repositoty)
        {
            _userRepository = userRepository;
        }

        public async Task<ProjectDto> CreateAsync(AddProjectDto dto)
        {
            var existedProject = await Repository.SingleAsync(s => s.Name == dto.Name);

            if (existedProject != null)
            {
                throw new BadRequestException("User is ready existed.");
            }

            var entity = dto.ToEntity();

            entity.SetAudit(AppContext.Value);

            var result = await CreateAsync(entity);

            return result?.ToDto();
        }

        public async Task<ProjectDto> GetDetailAsync(string projectName)
        {
            var entity = await Repository.SingleAsync(s => s.Name == projectName);

            return entity?.ToDto();
        }

        public async Task<IEnumerable<ProjectDto>> SearchAsync(string userName)
        {
            IEnumerable<Project> result = null;

            var user = await _userRepository.SingleAsync(s => s.UserName == userName);

            bool isSuperAdmin = user.Roles.Contains(RoleSetting.ROLE_ADMIN);

            result = true.Equals(isSuperAdmin) ? await ReadAllAsync() : user.AssignedProjects;

            result = result ?? new List<Project>();

            var projects = result.Select(s => s.ToDto());

            return projects;
        }
    }
}
