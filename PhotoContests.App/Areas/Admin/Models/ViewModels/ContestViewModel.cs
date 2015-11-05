namespace PhotoContests.App.Areas.Admin.Models.ViewModels
{
    using System;
    using Common.Mappings;
    using PhotoContests.Models;

    public class ContestViewModel : IMapTo<Contest>, IMapFrom<Contest>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEnded { get; set; }
    }
}