namespace TwoCS.TimeTracker.Authorization.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AspNet.Security.OAuth.Validation;
    using TwoCS.TimeTracker.Authorization.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    public class ResourceController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ResourceController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet("message")]
        public async Task<IActionResult> GetMessage()
        {
            var role = User.Claims.Where(s => s.Type == "role").FirstOrDefault()?.Value;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            return Content($"{user.UserName} has been successfully authenticated.");
        }
    }
}