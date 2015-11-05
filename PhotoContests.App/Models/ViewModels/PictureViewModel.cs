using System;
using System.Collections.Generic;

namespace PhotoContests.App.Models.ViewModels
{
    using System.Data.Entity.Migrations.Model;

    public class PictureViewModel
    {
        public int Id { get; set; }

        public string PictureUrl { get; set; }

        public int VoteCount { get; set; }

        public string Owner { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ContestDateEnded { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public int CommentsCount { get; set; }
    }
}