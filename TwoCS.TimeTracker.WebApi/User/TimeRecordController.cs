namespace TwoCS.TimeTracker.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Services;

    [Route("api/tracker")]
    [Authorize(Policy = "TimeRecordAdmin")]
    public class TimeRecordController : ApiBase
    {
        private readonly ITimeRecordService _timeRecordService;

        public TimeRecordController(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }

        
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AddTimeRecordDto dto)
        {
            var validator = CheckValidation(dto);

            if (!validator.IsValid) throw new BadRequestException(validator.Errors);

            var result = await _timeRecordService.CreateAsync(User.Identity.Name, dto);

            return ResultOk(result);
        }

        [HttpPost("logtimes")]
        public async Task<IActionResult> LogTime([FromBody] AddLogTimeRecordDto dto)
        {
            var validator = CheckValidation(dto);

            if (!validator.IsValid) throw new BadRequestException(validator.Errors);

            var result = await _timeRecordService.LogTimeAsync(User.Identity.Name, dto);

            return ResultOk(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Search()
        {
            var result = await _timeRecordService.SearchAsync(User.Identity.Name);

            return ResultOk(result);
        }
    }
}
