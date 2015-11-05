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

<<<<<<< HEAD
        public DateTime? DateEnded { get; set; }

        public string Owner { get; set; }

        public ICollection<PictureViewModel> Pictures { get; set; }

        public IEnumerable<WinnerPrizeViewModel> Winners { get; set; }
=======
        public string Owner { get; set; }

        public ICollection<PictureViewModel> Pictures { get; set; } 
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
    }
}