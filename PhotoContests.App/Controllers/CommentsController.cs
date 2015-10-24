using System.Linq;
using System.Web.Mvc;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(IPhotoContestsData data)
            : base(data)
        {
        }

        public CommentsController(IPhotoContestsData data, User user)
            : base(data, user)
        {
        }

        // GET: Comment
        public ActionResult Index()
        {
            var comments = this.Data.Comments.All().AsEnumerable()
                .Select(c => new
                {
                    Author = c.Author.UserName,
                    Picutre = c.Picture.PictureUrl,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt.ToString("dd-MM-yy HH:mm:ss")
                }).ToList();

            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return Content("Create");
        }

        public ActionResult Show()
        {
            return Content("Show");
        }

        public ActionResult Update()
        {
            return Content("Update");
        }

        public ActionResult Delete()
        {
            return Content("Delete");
        }
    }
}