using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContests.Data.Repositories;
using PhotoContests.Models;

namespace PhotoContests.Data.UnitOfWork
{
    public class PhotoContestsData : IPhotoContestsData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        private IUserStore<User> userStore;

        public PhotoContestsData(DbContext context)
        {
            this.dbContext = context;
            this.repositories = new Dictionary<Type, object>();
        }
        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Contest> Contests 
        {
            get
            {
                return this.GetRepository<Contest>();
            }
        }
        public IRepository<Picture> Pictures
        {
            get
            {
                return this.GetRepository<Picture>();
            }
        }

        public IRepository<Prize> Prizes
        {
            get
            {
                return this.GetRepository<Prize>();
            }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IUserStore<User> UserStore
        {
            get
            {
                if (this.userStore == null)
                {
                    this.userStore = new UserStore<User>(this.dbContext);
                }

                return this.userStore;
            }
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericEfRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
