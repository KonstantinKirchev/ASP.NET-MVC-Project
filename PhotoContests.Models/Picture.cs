using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContests.Models
{
    public class Picture
    {
        private ICollection<User> voters;
        private ICollection<Comment> comments;

        public Picture()
        {
            this.voters = new HashSet<User>();
            this.comments = new HashSet<Comment>();
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

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
