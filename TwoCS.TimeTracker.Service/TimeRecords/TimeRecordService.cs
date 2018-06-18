namespace TwoCS.TimeTracker.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Repositories;
    using TwoCS.TimeTracker.Core.Services;
    using TwoCS.TimeTracker.Core.Settings;
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

            var project = await _projectRepository.SingleAsync(s => s.Name == dto.ProjectId);

            var user = await _userRepository.SingleAsync(s => s.UserName == userName);

            if (project == null ||
                user == null)
            {
                throw new BadRequestException("Invalid project or user.");
            }

            var entity = dto.ToEntity();

            entity.SetAudit(AppContext.Value);

            entity.ProjectId = project.Id;

            entity.Project = project;

            entity.UserId = user.Id;

            entity.User = user;

            entity.LogTimeRecords = new List<LogTimeRecord>();

            entity = await CreateAsync(entity);

            return entity?.ToDto();
        }

        public async Task<LogTimeRecordDto> LogTimeAsync(string userName, AddLogTimeRecordDto dto)
        {
            var record = await Repository.ReadAsync(dto.TimeRecordId);

            var user = await _userRepository.SingleAsync(s => s.UserName == userName);

            if (record == null ||
                user ==  null)
            {
                throw new BadRequestException("Invalid time recoder or user.");
            }

            var logTimeEntity = dto.ToEntity();

            logTimeEntity.Id = UUID;

            logTimeEntity.UserId = user.Id;

            logTimeEntity.User = user;

            var logTimeRecords = record.LogTimeRecords ?? new List<LogTimeRecord>();

            logTimeRecords.Add(logTimeEntity);

            record.LogTimeRecords = logTimeRecords;

            await UpdateAsync(record);

            return logTimeEntity?.ToDto();
        }

        public async Task<IEnumerable<TimeRecordDto>> SearchAsync(string userName, string project)
        {
            var result = await ReadAllAsync();

            var user = await _userRepository.SingleAsync(s => s.UserName == userName);

            var userList = new List<string>() { userName };

            bool isSuperAdmin = user.Roles.Contains(RoleSetting.ROLE_ADMIN);

            if (user.Roles.Contains(RoleSetting.ROLE_MANAGER))
            {
                userList.AddRange(user.AssignedMembers?.Select(m => m.UserName));
            }

            return result?
                .Where(s => true.Equals(isSuperAdmin) || 
                    (userList.Contains(s.User.UserName) && 
                    (string.IsNullOrEmpty(project) || s.Project.Name == project)))
                .Select(s => s.ToDto());
        }

        public async Task<TimeRecordDto> DetailAsync(string guid)
        {
            var result = await ReadAsync(guid);

            return result?.ToDto();
        }
    }
}
