using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IPhotoContestsData data)
        {
            this.Data = data;
        }

        public BaseController(IPhotoContestsData data, User user)
            :this(data)
        {
            this.UserProfile = user;
        }

        public IPhotoContestsData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}