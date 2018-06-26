namespace TwoCS.TimeTracker.WebApi.OAuth
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Services;

    /// <summary>
    /// OAuth controller
    /// </summary>
    [Route("oauth")]
    public class OAuthController : ApiBase
    {
        private readonly IProxyService _proxyService;
        private readonly IUserService _userService;

        public OAuthController(IProxyService proxyService,
            IUserService userService)
        {
            _proxyService = proxyService;
            _userService = userService;
        }

        /// <summary>
        /// Get token for signing in an user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost("connect-token"), Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] LoginUserDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _proxyService.GetTokenAsync(dto.Email, dto.Password);

            var userInfo = await _userService.GetIdentityAsync(dto.Email);

            var output = new
            {
                identity = userInfo,
                token = result
            };

            return ResultOk(output);
        }

        
    }
}
