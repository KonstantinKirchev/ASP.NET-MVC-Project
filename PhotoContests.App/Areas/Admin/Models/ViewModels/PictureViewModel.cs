namespace PhotoContests.App.Areas.Admin.Models.ViewModels
{
    using Common.Mappings;
    using PhotoContests.Models;

    public class PictureViewModel : IMapTo<Picture>, IMapFrom<Picture>
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        public int Vote { get; set; }
        public int ContestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}