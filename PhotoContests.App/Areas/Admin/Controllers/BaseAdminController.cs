namespace PhotoContests.App.Areas.Admin.Controllers
{
    using App.Controllers;
    using Data.UnitOfWork;

    public class BaseAdminController : BaseController
    {
        public BaseAdminController(IPhotoContestsData data) 
            : base(data)
        {
        }
    }
}