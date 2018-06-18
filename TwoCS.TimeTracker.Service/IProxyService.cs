namespace TwoCS.TimeTracker.Services
{
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using TwoCS.TimeTracker.Dto;

    public interface IProxyService
    {
        Task<JObject> GetTokenAsync(string email, string password);

        Task<JObject> CreateAccountAsync(RegisterUserDto dto);

        Task<JObject> AddAccountToRoletAsync(string userName, string roleName);
    }
}
