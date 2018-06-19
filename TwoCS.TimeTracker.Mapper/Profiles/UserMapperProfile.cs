namespace TwoCS.TimeTracker.Mapper
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;

    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            #region Entity To Dto

            CreateMap<User, UserDto>()
                .ForMember(x => x.IsValid, opt => opt.Ignore())
                .ForMember(x => x.Errors, opt => opt.Ignore())
                .ForMember(x => x.AssignedMembers, opt => opt.Ignore())
                .ForMember(x => x.AssignedProjects, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    //TODO
                    dest.AssignedMembers = src.AssignedMembers?.Select(s => s.ToDto());

                    dest.AssignedProjects = src.AssignedProjects?.Select(s => s.ToDto());

                });
            #endregion

            #region Dto To Entity
            CreateMap<RegisterUserDto, User>()
              .AfterMap((src, dest) =>
              {
                    dest.Roles = new List<string> { src.Role ?? "User" };
                });
            #endregion
        }
    }

    public static class UserMapper
    {
        static UserMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }


        public static User ToEntity(this RegisterUserDto dto)
        {
            return Mapper.Map<User>(dto);
        }

        public static UserDto ToDto(this User entity)
        {
            return Mapper.Map<UserDto>(entity);
        }
    }
}
