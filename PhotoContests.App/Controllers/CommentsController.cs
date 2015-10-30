using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PhotoContests.App.Filters;
using PhotoContests.App.Hubs;
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
            var comments = this.Data.Comments.All().OrderByDescending(c => c.Id).ToList();

            var commentsModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            return Json(commentsModel, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        [HttpPost]
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            var comment = new Comment()
            {
                AuthorId = this.User.Identity.GetUserId(),
                Content = commentViewModel.Content,
                CreatedAt = DateTime.Now,
                PictureId = commentViewModel.PictureId
            };

            this.Data.Comments.Add(comment);

            if (this.Data.SaveChanges() > 0)
            {
                this.SendCommentWithSignalR(comment.Id);
            }
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
        private void SendCommentWithSignalR(int commentId)
        {
            var comment = this.Data.Comments.Find(commentId);
            var commentViewModel = Mapper.Map<Comment, CommentViewModel>(comment);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CommentsHub>();
            hubContext.Clients.All.receiveComment(commentViewModel);
        }
    }
}