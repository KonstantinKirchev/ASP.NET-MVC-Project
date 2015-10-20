using System.Linq;
using System.Web.Mvc;
using PhotoContests.App.Models.ViewModels;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestsData data)
            : base(data)
        {
        }

        public HomeController(IPhotoContestsData data, User user)
            : base(data, user)
        {
        }

        public ActionResult Index()
        {
            var contests = this.Data.Contests.All()
                .Select(c => new ContestViewModel()
                {
                    Title = c.Title
                });
            return View(contests);
        }
    }
}