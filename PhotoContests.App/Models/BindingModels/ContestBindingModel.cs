using System;
using System.ComponentModel.DataAnnotations;
using PhotoContests.Models;

namespace PhotoContests.App.Models.BindingModels
{
    public class ContestBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }

        public string Description { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }
    }
}