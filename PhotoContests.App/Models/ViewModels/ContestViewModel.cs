using System;
using System.Collections.Generic;
using PhotoContests.Common.Mappings;
using PhotoContests.Models;

namespace PhotoContests.App.Models.ViewModels
{
    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEnded { get; set; }

        public string Owner { get; set; }

        
    }
}