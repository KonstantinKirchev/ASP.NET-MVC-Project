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

        public int? ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public string WinnerId { get; set; }

        public virtual User Winner { get; set; }

        public int? WinnerPosition { get; set; }
    }
}
