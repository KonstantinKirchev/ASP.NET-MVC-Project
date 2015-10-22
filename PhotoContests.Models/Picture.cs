using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContests.Models
{
    public class Picture
    {
        private ICollection<User> voters;

        public Picture()
        {
            this.voters = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public int VoteCount { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<User> Voters
        {
            get { return this.voters; }
            set { this.voters = value; }
        } 
    }
}
