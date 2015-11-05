using PhotoContests.Data.Repositories;
using PhotoContests.Models;

namespace PhotoContests.Data.UnitOfWork
{
    public interface IPhotoContestsData
    {
        IRepository<User> Users { get; }

        IRepository<Contest> Contests { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<Prize> Prizes { get; }

<<<<<<< HEAD
        IRepository<Comment> Comments { get; }

        IRepository<Notification> Notifications { get; } 
=======
        IRepository<Comment> Comments { get; } 
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3

        int SaveChanges();
    }
}
