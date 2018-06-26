namespace TwoCS.TimeTracker.Services
{
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IUserService : IService<User>
    {
        Task<bool> SignOutAsync(string userName);

        Task<UserDto> RegisterAsync(RegisterUserDto dto);

        Task<IEnumerable<UserDto>> ManagerAsync(string userName);

        Task<IEnumerable<UserDto>> SearchAsync(string userName);

        Task<UserDto> PromoteManagerAsync(PromoteUserDto dto);

        Task<IEnumerable<string>> GetRolesAsync(string userName);

        Task<UserDto> GetDetailAsync(string userName);

        Task<UserDto> AssignMemberToManagerAsync(string userName, string managerName, string memberName);

        Task<UserDto> SignInAsManagerAsync(string userName, string managerName);

        Task<UserDto> AssignProjectAsync(string userName, string project, string memberName);

        Task<UserDto> GetIdentityAsync(string email);
    }
}
