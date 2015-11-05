using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoContests.App.Models.ViewModels;

namespace PhotoContests.App.Models.BindingModels
{
    public class ContestFinalBindingModel
    {
        public int? WinnersCount { get; set; }

        public DateTime? DateEnded { get; set; }

        public int? NumberOfParticipation { get; set; }

        [Display(Name = "Partisipator")]
        public ICollection<string> PartisipatorIds { get; set; }

        [Display(Name = "Voter")]
        public ICollection<string> VoterIds { get; set; }

        [Display(Name = "Prize")]
        public ICollection<int> PrizeIds { get; set; }
    }
}