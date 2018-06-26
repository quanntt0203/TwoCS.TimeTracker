namespace TwoCS.TimeTracker.Services
{
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Core.Services;
    using TwoCS.TimeTracker.Dto;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Mapper;
    using TwoCS.TimeTracker.Core.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Settings;

    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IProxyService _proxyService;
        private readonly IProjectRepository _projectRepository;

        public UserService(IUserRepository repository,
            IProxyService proxyService,
            IProjectRepository projectRepository)
            : base(repository)
        {
            _proxyService = proxyService;
            _projectRepository = projectRepository;
        }

        public async Task<bool> SignOutAsync(string userName)
        {
            var entity = await Repository.SingleAsync(s => s.UserName == userName);

            if (entity == null)
            {
                return false;
            }

            if (entity.BehaveOfMagager != null)
            {
                entity.BehaveOfMagager = null;
                await Repository.UpdateAsync(entity);
            }

            return true;
        }

        public async Task<UserDto> PromoteManagerAsync(PromoteUserDto dto)
        {
            var entity = await Repository.SingleAsync(s => s.UserName == dto.UserName);

            if (entity == null)
            {
                throw new BadRequestException("User is not existed.");
            }

            var roles = entity.Roles?.ToList() ?? new List<string>();

            if (roles.Contains(RoleSetting.ROLE_MANAGER))
            {
                throw new BadRequestException(string.Format("User is ready {0}.", RoleSetting.ROLE_MANAGER));
            }

            await _proxyService.AddAccountToRoletAsync(dto.UserName, RoleSetting.ROLE_MANAGER);

            roles.Add(RoleSetting.ROLE_MANAGER);

            entity.Roles = roles;

            await UpdateAsync(entity);

            return entity.ToDto();
        }

        public async Task<UserDto> AssignMemberToManagerAsync(string userName, string managerName, string memberName)
        {
            var filterNames = new string[] { userName, managerName, memberName };

            var users = await ReadAllAsync(s => filterNames.Contains(s.UserName));

            var admin = users.FirstOrDefault(s => s.UserName == userName && s.Roles.Contains(RoleSetting.ROLE_ADMIN));

            var manager = users?.FirstOrDefault(s => s.UserName == managerName && s.Roles.Contains(RoleSetting.ROLE_MANAGER));

            var member = users?.FirstOrDefault(s => s.UserName == memberName && s.Roles.Count() == 1 && s.Roles.Contains(RoleSetting.ROLE_USER));

            if (admin == null ||
                manager == null ||
                member == null ||
                member.ManagerId != null)
            {
                throw new BadRequestException("Invalid admin/manager or member.");
            }

            var assignedMembers = manager.AssignedMembers?.ToList() ?? new List<User>();

            if (assignedMembers.Exists(s => s.UserName == member.UserName))
            {
                throw new BadRequestException("Member is ready existed.");
            }

            // update manager first

            assignedMembers.Add(member);

            manager.AssignedMembers = assignedMembers;

            await Repository.UpdateAsync(manager);

            // update member
            member.ManagerId = manager.Id;

            //member.Magager = manager;

            await Repository.UpdateAsync(member);

            member.Magager = manager;

            return member.ToDto();
        }

        public async Task<UserDto> SignInAsManagerAsync(string userName, string managerName)
        {
            var filterNames = new string[] { userName, managerName };

            var users = await ReadAllAsync(s => filterNames.Contains(s.UserName));

            var admin = users.FirstOrDefault(s => s.UserName == userName && s.Roles.Contains(RoleSetting.ROLE_ADMIN));

            var manager = users?.FirstOrDefault(s => s.UserName == managerName && s.Roles.Contains(RoleSetting.ROLE_MANAGER));

            if (admin == null ||
                manager == null)
            {
                throw new BadRequestException("Invalid admin or manager.");
            }


            // temporary update members & projects as manager having
            admin.BehaveOfMagager = manager;

            await Repository.UpdateAsync(admin);

            return admin.ToDto();

        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            var existedUser = await Repository.SingleAsync(s => s.UserName == dto.UserName);

            if (existedUser != null)
            {
                throw new BadRequestException("User is ready existed.");
            }

            var entity = dto.ToEntity();

            var oauthUser = await _proxyService.CreateAccountAsync(dto);

            var user = await CreateAsync(entity);

            return user.ToDto();
        }

        public async Task<IEnumerable<UserDto>> SearchAsync(string userName)
        {
            var currentUser = await Repository.SingleAsync(s => s.UserName == userName);

            var isAdmin = currentUser?.Roles.Contains(RoleSetting.ROLE_ADMIN) ?? false;

            var users = true.Equals(isAdmin) ? (currentUser.BehaveOfMagager != null ? currentUser.BehaveOfMagager.AssignedMembers : await ReadAllAsync()) : currentUser.AssignedMembers;

            return users?.Where(s => s.UserName != userName)
                .Select(s => s.ToDto());
        }


        //ManagerAsync
        public async Task<IEnumerable<UserDto>> ManagerAsync(string userName)
        {
            var users = await ReadAllAsync(s => s.Roles.Contains(RoleSetting.ROLE_MANAGER));

            return users?.Select(s => s.ToDto());
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string userName)
        {
            var entity = await Repository.SingleAsync(s => s.UserName == userName);

            var roles = new List<string>();

            if (entity != null)
            {
                roles.AddRange(entity.Roles);
                if (entity.BehaveOfMagager != null)
                {
                    roles.AddRange(entity.BehaveOfMagager.Roles);
                }
            }

            roles.AddRange(entity.Roles);

            return roles.Distinct().ToArray();
        }

        public async Task<UserDto> GetDetailAsync(string userName)
        {
            var entiry = await Repository.SingleAsync(s => s.UserName == userName);

            if (entiry.ManagerId != null)
            {
                entiry.Magager = await Repository.ReadAsync(entiry.ManagerId);
            }

            return entiry?.ToDto();
        }

        public async Task<UserDto> GetIdentityAsync(string email)
        {
            var entiry = await Repository.SingleAsync(s => s.Email == email);

            if (entiry.ManagerId != null)
            {
                entiry.Magager = await Repository.ReadAsync(entiry.ManagerId);
            }

            return entiry?.ToDto();
        }


        public async Task<UserDto> AssignProjectAsync(string userName, string project, string memberName)
        {
            var filterNames = new string[] { userName, memberName };

            var users = await ReadAllAsync(s => filterNames.Contains(s.UserName));

            var admin = users.FirstOrDefault(s => s.UserName == userName &&
                    (s.Roles.Contains(RoleSetting.ROLE_ADMIN) || s.Roles.Contains(RoleSetting.ROLE_MANAGER))
                );

            var member = users?.FirstOrDefault(s => s.UserName == memberName &&
                    (s.Roles.Contains(RoleSetting.ROLE_USER) || s.Roles.Contains(RoleSetting.ROLE_MANAGER))
               );

            var assignProject = await _projectRepository.SingleAsync(s => s.Name == project);

            if (admin == null ||
                member == null ||
                assignProject == null)
            {
                throw new BadRequestException("Invalid admin/member or project.");
            }

            var assignedProjects = member.AssignedProjects?.ToList() ?? new List<Project>();

            if (assignedProjects.Exists(s => s.Name == project))
            {
                throw new BadRequestException("Project is ready existed.");
            }

            assignedProjects.Add(assignProject);

            member.AssignedProjects = assignedProjects;

            await Repository.UpdateAsync(member);

            return member.ToDto();
        }
    }
}
