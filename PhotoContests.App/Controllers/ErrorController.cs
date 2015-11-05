namespace PhotoContests.App.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }
        public ViewResult NotFound()
        {
            return View("NotFound");
        }
    }
}