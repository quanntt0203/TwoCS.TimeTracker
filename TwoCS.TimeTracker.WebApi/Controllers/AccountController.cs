namespace TwoCS.TimeTracker.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Services;

    [Route("api/account")]
    public class AccountController : ApiBase
    {
        private readonly IProxyService _proxyService;

        private readonly IUserService _userService;

        public AccountController(IProxyService proxyService,
            IUserService userService)
        {
            _proxyService = proxyService;
            _userService = userService;
        }

        [HttpPost("register-user")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {

            var validation = CheckValidation(dto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            var account = await _userService.RegisterAsync(dto);

            var result = await _proxyService.GetTokenAsync(account.Email, dto.Password);

            return ResultOk(result);
        }
    }
}
