﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoContests.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Contest> ownContests;
        private ICollection<Contest> postedPicturesContests;
        private ICollection<Contest> invitedContests;
        private ICollection<Contest> contestsForParticipation;
        private ICollection<Picture> pictures;
        private ICollection<Prize> contestPrizes;
        private ICollection<Prize> winPrizes;
        private ICollection<Comment> comments;
        private ICollection<Notification> notifications; 

        public User()
        {
            this.ownContests = new HashSet<Contest>();
            this.postedPicturesContests = new HashSet<Contest>();
            this.invitedContests = new HashSet<Contest>();
            this.contestsForParticipation = new HashSet<Contest>();
            this.pictures = new HashSet<Picture>();
            this.contestPrizes = new HashSet<Prize>();
            this.winPrizes = new HashSet<Prize>();
            this.comments = new HashSet<Comment>();
            this.notifications = new HashSet<Notification>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        [InverseProperty("PostedPictureOwners")]
        public virtual ICollection<Contest> PostedPicturesContests
        {
            get { return this.postedPicturesContests; }
            set { this.postedPicturesContests = value; }
        }

        [InverseProperty("InvitedUsersForVoting")]
        public virtual ICollection<Contest> InvitedContests
        {
            get { return this.invitedContests; }
            set { this.invitedContests = value; }
        }

        [InverseProperty("SelectedUsersForParticipation")]
        public virtual ICollection<Contest> ContestsForParticipation
        {
            get { return this.contestsForParticipation; }
            set { this.contestsForParticipation = value; }
        } 

        public virtual ICollection<Contest> OwnContests
        {
            get { return this.ownContests; }
            set { this.ownContests = value; }
        }         

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        public virtual ICollection<Prize> ContestPrizes
        {
            get { return this.contestPrizes; }
            set { this.contestPrizes = value; }
        }

        public virtual ICollection<Prize> WinPrizes
        {
            get { return this.winPrizes; }
            set { this.winPrizes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Notification> Notifications
        {
            get { return this.notifications; }
            set { this.notifications = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
