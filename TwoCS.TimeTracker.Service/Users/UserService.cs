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

        public UserService(IUserRepository repository,
            IProxyService proxyService)
            : base(repository)
        {
            _proxyService = proxyService;
        }

        public async Task<UserDto> PromoteManagerAsync(PromoteUserDto dto)
        {
            var entity = await Repository.SingleAsync(s => s.UserName == dto.UserName);

            if (entity == null)
            {
                throw new BadRequestException("User is not existed.");
            }

            var roles = entity.Roles ?? new List<string>();

            if (roles.Contains(RoleSetting.ROLE_MANAGER))
            {
                throw new BadRequestException(string.Format("User is ready {0}.", RoleSetting.ROLE_MANAGER));
            }

            roles.Add(RoleSetting.ROLE_MANAGER);

            await UpdateAsync(entity);

            return entity.ToDto();
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            var existedUser = await Repository.SingleAsync(s => s.UserName == dto.UserName);

            if(existedUser != null)
            {
                throw new BadRequestException("User is ready existed.");
            }

            var entity = dto.ToEntity();

            var oauthUser = await _proxyService.CreateAccountAsync(dto);

            var user = await CreateAsync(entity);

            return user.ToDto();
        }

        public async Task<IEnumerable<UserDto>> SearchAsync()
        {
            var result = await ReadAllAsync();

            return result?.Select(s => s.ToDto());
        }

        public async Task<string[]> GetRolesAsync(string userName)
        {
            var entiry = await Repository.SingleAsync(s => s.Email == userName || s.UserName == userName);

            return entiry?.Roles?.ToArray();
        }

        public async Task<UserDto> GetDetailAsync(string userName)
        {
            var entiry = await Repository.SingleAsync(s => s.Email == userName || s.UserName == userName);

            return entiry?.ToDto();
        }
    }
}
