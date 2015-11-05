using PhotoContests.Common.Mappings;
using PhotoContests.Models;

namespace PhotoContests.App.Models.ViewModels
{
    public class WinnerPrizeViewModel : IMapFrom<Prize>
    {
        public string Name { get; set; }

        public string WinnerUsername { get; set; }

        public string PictureUrl { get; set; }

        public int? WinnerPosition { get; set; }
    }
}