namespace TwoCS.TimeTracker.WebApi.OAuth
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Services;

    [Route("oauth")]
    public class OAuthController : ApiBase
    {
        private readonly IProxyService _proxyService;

        public OAuthController(IProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        [HttpPost("connect-token"), Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] LoginUserDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _proxyService.GetTokenAsync(dto.Email, dto.Password);

            return ResultOk(result);
        }

        
    }
}
