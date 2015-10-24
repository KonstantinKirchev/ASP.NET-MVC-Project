using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
                .OrderByDescending(c => c.DateCreated)
                .Where(c => c.IsClosed == false);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
        }
    }
}