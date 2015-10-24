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

        IRepository<Comment> Comments { get; } 

        void SaveChanges();
    }
}
