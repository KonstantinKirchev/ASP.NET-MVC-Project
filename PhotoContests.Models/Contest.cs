using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoContests.Models
{
    public class Contest
    {
        private ICollection<User> postedPictureOwners;
        private ICollection<Picture> contestPictures;
        private ICollection<User> invitedUsersForVoting;
        private ICollection<User> selectedUsersForParticipation;
        private ICollection<User> contestWinners;
        private ICollection<Prize> prizes;

        public Contest()
        {
            this.postedPictureOwners = new HashSet<User>();
            this.contestPictures = new HashSet<Picture>();
            this.invitedUsersForVoting = new HashSet<User>();
            this.selectedUsersForParticipation = new HashSet<User>();
            this.contestWinners = new HashSet<User>();
            this.prizes = new HashSet<Prize>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEnded { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }

        public int? Winners { get; set; }

        public int? MaxParticipants { get; set; }

        [Required]
        public string ContestOwnerId { get; set; }

        public virtual User ContestOwner { get; set; }

        [InverseProperty("PostedPicturesContests")]
        public virtual ICollection<User> PostedPictureOwners
        {
            get { return this.postedPictureOwners; }
            set { this.postedPictureOwners = value; }
        }

        public virtual ICollection<Picture> ContestPictures
        {
            get { return this.contestPictures; }
            set { this.contestPictures = value; }
        }

        [InverseProperty("InvitedContests")]
        public virtual ICollection<User> InvitedUsersForVoting
        {
            get { return this.invitedUsersForVoting; }
            set { this.invitedUsersForVoting = value; }
        }

        [InverseProperty("ContestsForParticipation")]
        public virtual ICollection<User> SelectedUsersForParticipation
        {
            get { return this.selectedUsersForParticipation; }
            set { this.selectedUsersForParticipation = value; }
        }

        public virtual ICollection<User> ContestWinners
        {
            get { return this.contestWinners; }
            set { this.contestWinners = value; }
        }

        public virtual ICollection<Prize> Prizes
        {
            get { return this.prizes; }
            set { this.prizes = value; }
        }

        public bool IsClosed { get; set; }
    }
}
