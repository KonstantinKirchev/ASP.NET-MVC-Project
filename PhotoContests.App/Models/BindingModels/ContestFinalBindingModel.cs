using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoContests.App.Models.ViewModels;
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3

namespace PhotoContests.App.Models.BindingModels
{
    public class ContestFinalBindingModel
    {
        public int? WinnersCount { get; set; }

        public DateTime? DateEnded { get; set; }

        public int? NumberOfParticipation { get; set; }
<<<<<<< HEAD

        [Display(Name = "Partisipator")]
        public ICollection<string> PartisipatorIds { get; set; }

        [Display(Name = "Voter")]
        public ICollection<string> VoterIds { get; set; }

        [Display(Name = "Prize")]
        public ICollection<int> PrizeIds { get; set; }
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
    }
}