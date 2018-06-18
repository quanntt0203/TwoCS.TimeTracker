namespace TwoCS.TimeTracker.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Services;

    [Route("api/user")]
    public class UserController : ApiBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "UserAdmin")]
        public async Task<IActionResult> Search()
        {
            var result = await _userService.SearchAsync(User.Identity.Name);

            return ResultOk(result);
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpPost("promotions")]
        public async Task<IActionResult> Promote([FromBody] PromoteUserDto dto)
        {
            var validation = CheckValidation(dto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            var result = await _userService.PromoteManagerAsync(dto);

            return ResultOk(result);
        }
    }
}
