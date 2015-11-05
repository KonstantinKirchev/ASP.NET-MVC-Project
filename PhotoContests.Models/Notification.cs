using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoContests.Models
{
    public class Notification
    {
        public Notification()
        {
            this.DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsViewed { get; set; }
    }
}
