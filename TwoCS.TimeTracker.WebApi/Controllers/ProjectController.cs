namespace TwoCS.TimeTracker.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Services;

    [Route("api/project"), Produces("application/json")]
    public class ProjectController : ApiBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Policy = "ProjectAdmin")]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AddProjectDto dto)
        {
            var validator = CheckValidation(dto);

            if (!validator.IsValid) throw new BadRequestException(validator.Errors);

            var result = await _projectService.CreateAsync(dto);

            return ResultOk(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Search()
        {
            var result = await _projectService.SearchAsync(User.Identity.Name);

            return ResultOk(result);
        }
    }
}
