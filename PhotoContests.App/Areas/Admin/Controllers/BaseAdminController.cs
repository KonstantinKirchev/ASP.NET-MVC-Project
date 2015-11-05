using System.Web.Mvc;

namespace PhotoContests.App.Areas.Admin.Controllers
{
    using App.Controllers;
    using Data.UnitOfWork;

    [Authorize(Roles = "Admin")]
    public class BaseAdminController : BaseController
    {
        public BaseAdminController(IPhotoContestsData data) 
            : base(data)
        {
        }
    }
}