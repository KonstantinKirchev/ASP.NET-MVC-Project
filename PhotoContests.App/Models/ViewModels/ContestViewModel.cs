using System;
using System.Collections.Generic;
<<<<<<< HEAD
using PhotoContests.Common.Mappings;
using PhotoContests.Models;

namespace PhotoContests.App.Models.ViewModels
{
    public class ContestViewModel : IMapFrom<Contest>
=======

namespace PhotoContests.App.Models.ViewModels
{
    public class ContestViewModel
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEnded { get; set; }

        public string Owner { get; set; }
<<<<<<< HEAD

        
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
    }
}