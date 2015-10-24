using System.Collections.Generic;

namespace PhotoContests.App.Models.ViewModels
{
    public class PictureViewModel
    {
        public int Id { get; set; }

        public string PictureUrl { get; set; }

        public int VoteCount { get; set; }

        public string Owner { get; set; }
    }
}