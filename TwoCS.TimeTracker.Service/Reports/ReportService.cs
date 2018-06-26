namespace TwoCS.TimeTracker.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Settings;
    using TwoCS.TimeTracker.Core.UoW;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto.Reports;

    public class ReportService :  IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReportDto> DailyReportAsync(string userName, ReportParamDto dto)
        {
            var reportData = new List<DailyReportDto>();

            var user = await GetUserAsync(userName);

            // TODO: Report detail
            var logTimes = await GetReportDataAsync(user, dto);

            switch (dto.GroupBy)
            {
                default:
                    break;

                case ReportGroupTypeEnum.Project:
                    {
                        var rptData =
                            from u in logTimes
                            group u by new
                            {
                                u.Project,
                                u.LogDate
                            } into gcs
                            select new DailyReportDto
                            {
                                Project = gcs.Key.Project,
                                LogDate = gcs.Key.LogDate,
                                Duration = gcs.Sum(s => s.Duration)
                            };


                        reportData.AddRange(rptData);
                        break;
                    }

                case ReportGroupTypeEnum.User:
                    {
                        var rptData =
                            from u in logTimes
                            group u by new
                            {
                                u.User,
                                u.LogDate
                            } into gcs
                            select new DailyReportDto
                            {
                                User = gcs.Key.User,
                                LogDate  = gcs.Key.LogDate,
                                Duration = gcs.Sum(s => s.Duration)
                            };

                        reportData.AddRange(rptData);

                        break;
                    }
            }

            reportData.ForEach(r => {
                r.IsMarked = r.Duration < ReportLimitionHours.DayHours;
            });

            return new ReportDto
            {
                Records = reportData,
                Param = dto,
                ReportType = ReportTypeEnum.Daily
            };
        }

        public async Task<IReportDto> MonthlyReportAsync(string userName, ReportParamDto dto)
        {
            var reportData = new List<MonthlyReportDto>();

            var user = await GetUserAsync(userName);

            // TODO: Report detail
            var logTimes = await GetReportDataAsync(user, dto);

            switch (dto.GroupBy)
            {
                default:
                    break;

                case ReportGroupTypeEnum.Project:
                    {
                        var rptData =
                            from u in logTimes
                            group u by new
                            {
                                u.Project,
                                u.MonthName
                            } into gcs
                            select new MonthlyReportDto
                            {
                                Project = gcs.Key.Project,
                                MonthName = gcs.Key.MonthName,
                                Duration = gcs.Sum(s => s.Duration)
                            };


                        reportData.AddRange(rptData);
                        break;
                    }

                case ReportGroupTypeEnum.User:
                    {
                        var rptData =
                            from u in logTimes
                            group u by new
                            {
                                u.User,
                                u.MonthName
                            } into gcs
                            select new MonthlyReportDto
                            {
                                User = gcs.Key.User,
                                MonthName = gcs.Key.MonthName,
                                Duration = gcs.Sum(s => s.Duration)
                            };

                        reportData.AddRange(rptData);

                        break;
                    }
            }

            reportData.ForEach(r => {
                r.IsMarked = r.Duration < ReportLimitionHours.MonthHours;
            });

            return new ReportDto
            {
                Records = reportData,
                Param = dto,
                ReportType = ReportTypeEnum.Monthly
            };

        }

        public async Task<IReportDto> WeeklyReportAsync(string userName, ReportParamDto dto)
        {
            var reportData = new List<WeeklyReportDto>();

            var user = await GetUserAsync(userName);

            // TODO: Report detail
            var logTimes = await GetReportDataAsync(user, dto);

            switch (dto.GroupBy)
            {
                default:
                    break;

                case ReportGroupTypeEnum.Project:
                    {
                        var rptData =
                            from u in logTimes
                            group u by new
                            {
                                u.Project,
                                u.WeekName
                            } into gcs
                            select new WeeklyReportDto
                            {
                                Project = gcs.Key.Project,
                                WeekName = gcs.Key.WeekName,
                                Duration = gcs.Sum(s => s.Duration)
                            };


                        reportData.AddRange(rptData);
                        break;
                    }

                case ReportGroupTypeEnum.User:
                    {
                        var rptData =
                            from u in logTimes
                            group u by new
                            {
                                u.User,
                                u.WeekName
                            } into gcs
                            select new WeeklyReportDto
                            {
                                User = gcs.Key.User,
                                WeekName = gcs.Key.WeekName,
                                Duration = gcs.Sum(s => s.Duration)
                            };

                        reportData.AddRange(rptData);

                        break;
                    }
            }

            reportData.ForEach(r => {
                r.IsMarked = r.Duration < ReportLimitionHours.WeekHours;
            });

            return new ReportDto
            {
                Records = reportData,
                Param = dto,
                ReportType = ReportTypeEnum.Weekly
            };
        }

        private async Task<IEnumerable<LogTimeTransformDataDto>> GetReportDataAsync(User user, ReportParamDto dto)
        {
            var isAdmin = user.Roles.Contains(RoleSetting.ROLE_ADMIN);

            // TODO: Report detail
            var projects = (true.Equals(isAdmin) ? user.BehaveOfMagager?.AssignedProjects : user.AssignedProjects)?.Select(s => s.Name);

            var records = projects?.Count() > 0 ? await _unitOfWork.TimeRecordRepository.ReadAllAsync(s => projects.Contains(s.Project.Name))
                : await _unitOfWork.TimeRecordRepository.ReadAllAsync();

            if (!string.IsNullOrEmpty(dto.Project))
            {
                records = records.Where(s => s.Project.Name == dto.Project);
            }

            var logTimes =  new List<LogTimeTransformDataDto>();

            records.ToList()
                .ForEach(s => {

                    var items = s.LogTimeRecords.Select(l => new LogTimeTransformDataDto {
                        Project = s.Project.Name,
                        User = l.User.UserName,
                        LogDate = l.LogTime.Date,
                        WeekName = GetWeekOfYear(l.LogTime),
                        MonthName = GetMonthOfYear(l.LogTime),
                        Duration = l.Duration
                    });

                    logTimes.AddRange(items);
            });

            IEnumerable<LogTimeTransformDataDto> logRows = logTimes;

            if (!string.IsNullOrEmpty(dto.User))
            {
                logRows = logRows.Where(s => s.User == dto.User);
            }

            if (dto.StartDate.HasValue)
            {
                logRows = logRows.Where(s => s.LogDate >= dto.StartDate && s.LogDate <= dto.EndDate);
            }

            return logRows;
        }


        private async Task<User> GetUserAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.SingleAsync(s => s.UserName == userName);

            if (user == null)
            {
                throw new BadRequestException("Invalid report user.");
            }

            return user;
        }

        private string GetWeekOfYear(DateTime logDate)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            var weekNumber = cal.GetWeekOfYear(logDate, dfi.CalendarWeekRule,
                                          dfi.FirstDayOfWeek);

            return string.Format("Week {0}", weekNumber);

        }

        private string GetMonthOfYear(DateTime logDate)
        {
            return logDate.ToString("MMMM");

        }
    }
}
