namespace TwoCS.TimeTracker.Services
{
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IUserService : IService<User>
    {
        Task<UserDto> RegisterAsync(RegisterUserDto dto);

        Task<IEnumerable<UserDto>> SearchAsync(string userName);

        Task<UserDto> PromoteManagerAsync(PromoteUserDto dto);

        Task<string[]> GetRolesAsync(string userName);

        Task<UserDto> GetDetailAsync(string userName);

        Task<UserDto> AssignMemberToManagerAsync(string userName, string managerName, string memberName);

        Task<UserDto> SignInAsManagerAsync(string userName, string managerName);
    }
}
