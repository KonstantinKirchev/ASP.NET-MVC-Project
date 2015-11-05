using System.ComponentModel.DataAnnotations;
using PhotoContests.Common.Mappings;
using PhotoContests.Models;

namespace PhotoContests.App.Models.BindingModels
{
    public class PrizeBindingModel : IMapTo<Prize>
    {
        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string WinnerId { get; set; }
    }
}