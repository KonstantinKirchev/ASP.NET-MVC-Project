using System.ComponentModel.DataAnnotations;

namespace PhotoContests.Models
{
    public class Prize
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }
}
