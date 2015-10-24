using System.Collections.Generic;
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

            Mapper.CreateMap<Picture, PictureViewModel>()
                .ForMember(model => model.Owner, config => config.MapFrom(picture => picture.Owner.UserName));

            Mapper.CreateMap<Contest, ContestDetailViewModel>()
                .ForMember(model => model.Owner, config => config.MapFrom(contest => contest.ContestOwner.UserName))
                .ForMember(model => model.Pictures, config => config.MapFrom(contest => Mapper.Map<ICollection<Picture>, ICollection<PictureViewModel>>(contest.ContestPictures)));

        }
    }
}