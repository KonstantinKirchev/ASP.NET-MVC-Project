using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using PhotoContests.App.Models.ViewModels;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    [Authorize]
    public class NotificationsController : BaseController
    {
        public NotificationsController(IPhotoContestsData data)
            : base(data)
        {
        }

        public NotificationsController(IPhotoContestsData data, User user)
            : base(data, user)
        {
        }

        // GET: Notifications
        public ActionResult Index()
        {
            var userId = this.UserProfile.Id;

            var notifications = this.Data.Notifications.All()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateCreated)
                .ProjectTo<NotificationViewModel>();

            return View(notifications);
        }
    }
}