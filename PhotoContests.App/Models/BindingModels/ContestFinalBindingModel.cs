using System;

namespace PhotoContests.App.Models.BindingModels
{
    public class ContestFinalBindingModel
    {
        public int? WinnersCount { get; set; }

        public DateTime? DateEnded { get; set; }

        public int? NumberOfParticipation { get; set; }
    }
}