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

        /// <summary>
        /// User signout
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "UserAdmin")]
        [HttpGet("signout")]
        public async Task<IActionResult> SignOutAsync()
        {
            var result = await _userService.SignOutAsync(User.Identity.Name);

            return ResultOk(result);
        }


        /// <summary>
        /// Admin user gets all users with manage role
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("managers")]
        public async Task<IActionResult> ManagerAsync()
        {
            var result = await _userService.ManagerAsync(User.Identity.Name);

            return ResultOk(result);
        }


        /// <summary>
        /// Admin user gets all assigned users
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "UserAdmin")]
        [HttpGet()]
        public async Task<IActionResult> SearchAsync()
        {
            var result = await _userService.SearchAsync(User.Identity.Name);

            return ResultOk(result);
        }

        /// <summary>
        /// Admin user gets user detail by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [Authorize(Policy = "UserAdmin")]
        [HttpGet("details/{userName}")]
        public async Task<IActionResult> DetailAsync(string userName)
        {
            var result = await _userService.GetDetailAsync(userName);

            return ResultOk(result);
        }

        /// <summary>
        /// Admin promotes an user to manager
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = "SuperAdmin")]
        [HttpPost("promotions")]
        public async Task<IActionResult> PromoteAsycn([FromBody] PromoteUserDto dto)
        {
            var validation = CheckValidation(dto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            var result = await _userService.PromoteManagerAsync(dto);

            return ResultOk(result);
        }


        /// <summary>
        /// Admin assigns an user to a manager
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = "SuperAdmin")]
        [HttpPost("assignment-members")]
        public async Task<IActionResult> AssignMemberAsync([FromBody] AssignUserDto dto)
        {
            var validation = CheckValidation(dto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            var result = await _userService.AssignMemberToManagerAsync(User.Identity.Name, dto.Manager, dto.Member);

            return ResultOk(result);
        }

        /// <summary>
        /// Project admin assigns a project to a member
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = "ProjectAdmin")]
        [HttpPost("assignment-projects")]
        public async Task<IActionResult> AssignProjectAsync([FromBody] AssignProjectUserDto dto)
        {
            var validation = CheckValidation(dto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            var result = await _userService.AssignProjectAsync(User.Identity.Name, dto.Project, dto.Member);

            return ResultOk(result);
        }


        /// <summary>
        /// Admin sign-in as a manager role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = "SuperAdmin")]
        [HttpPost("signIn-managers")]
        public async Task<IActionResult> SignInAsManagerAsync([FromBody] SignInManagerDto dto)
        {
            var validation = CheckValidation(dto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            var result = await _userService.SignInAsManagerAsync(User.Identity.Name, dto.Manager);

            return ResultOk(result);
        }
    }
}
