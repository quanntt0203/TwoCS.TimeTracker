namespace TwoCS.TimeTracker.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Dto.Reports;
    using TwoCS.TimeTracker.Services;

    [Route("api/reports")]
    [Authorize(Policy = "ReportAdmin")]
    public class ReportController : ApiBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        
        [HttpGet()]
        public async Task<IActionResult> SearchAsync(
            [FromQuery] ReportTypeEnum reportType,
            [FromQuery] string project,
            [FromQuery] string user,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {

            var paramDto = new ReportParamDto
            {
                ReportType = reportType,
                Project = project,
                User = user,
                StartDate = startDate,
                EndDate = endDate
            };

            var validation = CheckValidation(paramDto);

            if (!validation.IsValid) throw new BadRequestException(validation.Errors);

            IReportDto result = new ReportDto();

            switch (reportType)
            {
                default:
                    {
                        break;
                    }

                case ReportTypeEnum.Daily:
                    {
                        result = await _reportService.DailyReportAsync(User.Identity.Name, paramDto);
                        break;
                    }

                case ReportTypeEnum.Weekly:
                    {
                        result = await _reportService.WeeklyReportAsync(User.Identity.Name, paramDto);
                        break;
                    }

                case ReportTypeEnum.Monthly:
                    {
                        result = await _reportService.MonthlyReportAsync(User.Identity.Name, paramDto);
                        break;
                    }
            }

            return ResultOk(result);
        }
    }
}
