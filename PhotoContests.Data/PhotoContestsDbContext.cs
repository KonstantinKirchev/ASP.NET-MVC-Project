using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhotoContests.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class PhotoContestsDbContext : IdentityDbContext<User>
    {
        public PhotoContestsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Contest> Contests { get; set; }

        public virtual IDbSet<Picture> Pictures { get; set; }

        public virtual IDbSet<Prize> Prizes { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public static PhotoContestsDbContext Create()
        {
            return new PhotoContestsDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.PostedPictureOwners)
                .WithMany(u => u.PostedPicturesContests)
                .Map(c =>
                {
                    c.MapLeftKey("PostedPictureOwnerId");
                    c.MapRightKey("PostedPicturesContestId");
                    c.ToTable("PostedPictureOwnersPostedPicturesContests");
                });

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.InvitedUsersForVoting)
                .WithMany(u => u.InvitedContests)
                .Map(c =>
                {
                    c.MapLeftKey("InvitedUserForVotingId");
                    c.MapRightKey("InvitedContestId");
                    c.ToTable("InvitedUsersForVotingInvitedContests");
                });

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.SelectedUsersForParticipation)
                .WithMany(u => u.ContestsForParticipation)
                .Map(c =>
                {
                    c.MapLeftKey("SelectedUserForParticipationId");
                    c.MapRightKey("ContestForParticipationId");
                    c.ToTable("SelectedUsersForParticipationContestsForParticipation");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.OwnContests)
                .WithRequired(c => c.ContestOwner);

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.ContestPictures)
                .WithRequired(p => p.Contest);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pictures)
                .WithMany(p => p.Voters)
                .Map(u =>
                {
                    u.MapLeftKey("VoterId");
                    u.MapRightKey("PictureId");
                    u.ToTable("VotersPictures");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
