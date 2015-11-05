<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace PhotoContests.App.Models.ViewModels
{
    using System.Data.Entity.Migrations.Model;

=======
﻿using System.Collections.Generic;

namespace PhotoContests.App.Models.ViewModels
{
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
    public class PictureViewModel
    {
        public int Id { get; set; }

        public string PictureUrl { get; set; }

        public int VoteCount { get; set; }

        public string Owner { get; set; }

<<<<<<< HEAD
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ContestDateEnded { get; set; }

=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
        public ICollection<CommentViewModel> Comments { get; set; }

        public int CommentsCount { get; set; }
    }
}