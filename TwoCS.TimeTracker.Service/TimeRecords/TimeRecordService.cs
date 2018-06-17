namespace TwoCS.TimeTracker.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Repositories;
    using TwoCS.TimeTracker.Core.Services;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;
    using TwoCS.TimeTracker.Mapper;

    public class TimeRecordService : ServiceBase<TimeRecord>, ITimeRecordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogTimeRecordRepository _logTimeRecordRepository;

        public TimeRecordService(
            ITimeRecordRepository repository,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            ILogTimeRecordRepository logTimeRecordRepository)
            : base(repository)

        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _logTimeRecordRepository = logTimeRecordRepository;
        }

        public async Task<TimeRecordDto> CreateAsync(string userName, AddTimeRecordDto dto)
        {

            var project = await _projectRepository.ReadAsync(dto.ProjectId);

            var user = await _userRepository.SingleAsync(s => s.Email == userName);

            if (project == null ||
                user == null)
            {
                throw new BadRequestException("Invalid project or user.");
            }

            var entity = dto.ToEntity();

            entity.Project = project;

            entity.User = user;

            entity = await CreateAsync(entity);

            return entity?.ToDto();
        }

        public async Task<LogTimeRecordDto> LogTimeAsync(string userName, AddLogTimeRecordDto dto)
        {
            var record = await Repository.ReadAsync(dto.TimeRecordId);

            var user = await _userRepository.SingleAsync(s => s.Email == userName);

            if (record == null ||
                user ==  null)
            {
                throw new BadRequestException("Invalid time recoder or user.");
            }

            var logTimeEntity = dto.ToEntity();

            logTimeEntity.UserId = user.Id;

            logTimeEntity.User = user;

            logTimeEntity.TimeRecord = record;

            logTimeEntity = await _logTimeRecordRepository.CreateAsync(logTimeEntity);

            return logTimeEntity?.ToDto();
        }

        public async Task<IEnumerable<TimeRecordDto>> SearchAsync(string userName)
        {
            var result = await ReadAllAsync();

            return result?.Select(s => s.ToDto());
        }

    }
}
