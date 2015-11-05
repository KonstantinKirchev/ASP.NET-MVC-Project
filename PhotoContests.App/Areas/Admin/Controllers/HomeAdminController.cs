using System.Web.Mvc;
using PhotoContests.Data.UnitOfWork;

namespace PhotoContests.App.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {
        public HomeAdminController(IPhotoContestsData data)
            : base(data)
        {
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}