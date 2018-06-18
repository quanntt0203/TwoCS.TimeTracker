namespace TwoCS.TimeTracker.Mapper
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Models;
    using Dto;

    public class TimeRecordMapperProfile : Profile
    {
        public TimeRecordMapperProfile()
        {
            #region Entity To Dto

            CreateMap<TimeRecord, TimeRecordDto>()
                .ForMember(x => x.Project, opt => opt.MapFrom(src => src.Project.ToDto()))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User.ToDto()))
                .AfterMap((src, dest) =>
                {
                    //TODO
                    //dest.Duration = (int)(src.EndTime.HasValue ? (src.StartTime - src.EndTime.Value).TotalHours : 0);
                    dest.LogTimeRecords = (src.LogTimeRecords?.Select(s => s.ToDto()).ToList()) ?? new List<LogTimeRecordDto>();
                    dest.Duration = (src.LogTimeRecords?.Sum(s => s.Duration) ?? 0);
                });

            CreateMap<LogTimeRecord, LogTimeRecordDto>()
               .ForMember(x => x.User, opt => opt.MapFrom(src => src.User.ToDto()))
               .AfterMap((src, dest) =>
               {
                    //TODO
                    
               });
            #endregion

            #region Dto To Entity
            CreateMap<AddTimeRecordDto, TimeRecord>()
              .ForMember(x => x.StartDate, opt => opt.Ignore())
              .ForMember(x => x.EndDate, opt => opt.Ignore())
              .ForMember(x => x.CapturedInfo, opt => opt.Ignore())
              .ForMember(x => x.Project, opt => opt.Ignore())
              .ForMember(x => x.User, opt => opt.Ignore())
              .AfterMap((src, dest) =>
              {
                  //TODO
              });

            CreateMap<AddLogTimeRecordDto, LogTimeRecord>()
              .ForMember(x => x.UserId, opt => opt.Ignore())
              .ForMember(x => x.TimeRecord, opt => opt.Ignore())
              .ForMember(x => x.User, opt => opt.Ignore())
              .AfterMap((src, dest) =>
              {
                  //TODO
              });
            #endregion
        }
    }

    public static class TimeRecordMapper
    {
        static TimeRecordMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<TimeRecordMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }


        public static TimeRecord ToEntity(this AddTimeRecordDto dto)
        {
            return Mapper.Map<TimeRecord>(dto);
        }

        public static TimeRecordDto ToDto(this TimeRecord entity)
        {
            return Mapper.Map<TimeRecordDto>(entity);
        }

        // Log time record
        public static LogTimeRecord ToEntity(this AddLogTimeRecordDto dto)
        {
            return Mapper.Map<LogTimeRecord>(dto);
        }

        public static LogTimeRecordDto ToDto(this LogTimeRecord entity)
        {
            return Mapper.Map<LogTimeRecordDto>(entity);
        }
    }
}
