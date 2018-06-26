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


        /// <summary>
        /// User runs the time tracker report
        /// </summary>
        /// <param name="reportType">Report will be displayed by daily, weekly or monthly.</param>
        /// <param name="groupType">Report will be grouped by project or user.</param>
        /// <param name="project">The selected project for filtering report.</param>
        /// <param name="user">The selected user for filtering report.</param>
        /// <param name="startDate">The start date value for filtering report.</param>
        /// <param name="endDate">The end date value for filtering report.</param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> SearchAsync(
            [FromQuery] ReportTypeEnum reportType,
            [FromQuery] ReportGroupTypeEnum groupType,
            [FromQuery] string project,
            [FromQuery] string user,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {

            var paramDto = new ReportParamDto
            {
                ReportType = reportType,
                GroupBy = groupType,
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
