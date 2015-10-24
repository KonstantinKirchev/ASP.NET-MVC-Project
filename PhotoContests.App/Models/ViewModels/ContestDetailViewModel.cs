namespace PhotoContests.App.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class ContestDetailViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string Owner { get; set; }

        public ICollection<PictureViewModel> Pictures { get; set; } 
    }
}