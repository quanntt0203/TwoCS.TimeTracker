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

        
        /// <summary>
        /// User creates a new time tracker record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AddTimeRecordDto dto)
        {
            var validator = CheckValidation(dto);

            if (!validator.IsValid) throw new BadRequestException(validator.Errors);

            var result = await _timeRecordService.CreateAsync(User.Identity.Name, dto);

            return ResultOk(result);
        }

        /// <summary>
        /// User logs a time duration
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost("logtimes")]
        public async Task<IActionResult> LogTime([FromBody] AddLogTimeRecordDto dto)
        {
            var validator = CheckValidation(dto);

            if (!validator.IsValid) throw new BadRequestException(validator.Errors);

            var result = await _timeRecordService.LogTimeAsync(User.Identity.Name, dto);

            return ResultOk(result);
        }

        /// <summary>
        /// User gets all time tracker records 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> Search([FromQuery] string project)
        {
            var result = await _timeRecordService.SearchAsync(User.Identity.Name, project);

            return ResultOk(result);
        }


        /// <summary>
        /// User gets time tracker record in detail
        /// </summary>
        /// <param name="id">Value of time tracker record</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var result = await _timeRecordService.DetailAsync(id);

            return ResultOk(result);
        }
    }
}
