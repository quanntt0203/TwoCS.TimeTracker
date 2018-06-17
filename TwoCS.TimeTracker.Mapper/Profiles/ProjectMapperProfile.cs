namespace TwoCS.TimeTracker.Mapper
{
    using AutoMapper;
    using Domain.Models;
    using Dto;

    public class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            #region Entity To Dto

            CreateMap<User, ProjectDto>()
                .ForMember(x => x.OrderNo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    //TODO
                });
            #endregion

            #region Dto To Entity
            CreateMap<AddProjectDto, Project>()
              .ForMember(x => x.OrderNo, opt => opt.Ignore())
              .AfterMap((src, dest) =>
              {
                  //TODO
              });
            #endregion
        }
    }

    public static class ProjectDtoMapper
    {
        static ProjectDtoMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProjectMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }


        public static Project ToEntity(this AddProjectDto dto)
        {
            return Mapper.Map<Project>(dto);
        }

        public static ProjectDto ToDto(this Project entity)
        {
            return Mapper.Map<ProjectDto>(entity);
        }
    }
}
