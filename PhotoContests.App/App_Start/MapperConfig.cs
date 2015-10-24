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
                .ForMember(model => model.Owner, config => config.MapFrom(picture => picture.Owner.UserName))
                .ForMember(model => model.Comments, config => config.MapFrom(picture => Mapper.Map<ICollection<Comment>, ICollection<CommentViewModel>>(picture.Comments)));

            Mapper.CreateMap<Contest, ContestDetailViewModel>()
                .ForMember(model => model.Owner, config => config.MapFrom(contest => contest.ContestOwner.UserName))
                .ForMember(model => model.Pictures,
                    config =>
                        config.MapFrom(
                            contest =>
                                Mapper.Map<ICollection<Picture>, ICollection<PictureViewModel>>(contest.ContestPictures)));

            Mapper.CreateMap<Comment, CommentViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(comment => comment.Author.UserName));
        }
    }
}