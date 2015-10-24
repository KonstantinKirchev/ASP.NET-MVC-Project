using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PhotoContests.App.Models.ViewModels;
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
            var comments = this.Data.Comments.All().ToList();

            var commentsModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            return Json(commentsModel, JsonRequestBehavior.AllowGet);
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