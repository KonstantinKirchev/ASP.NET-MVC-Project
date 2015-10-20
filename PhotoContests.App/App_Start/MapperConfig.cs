using AutoMapper;
using PhotoContests.App.Models.ViewModels;
using PhotoContests.Models;

namespace PhotoContests.App.App_Start
{
    public static class MapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Contest, ContestViewModel>()
                .ForMember(model => model.Owner, config => config.MapFrom(contest => contest.ContestOwner.UserName));
        }
    }
}