using System.Linq;

namespace PhotoContests.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models.ViewModels;
    using PhotoContests.Models;

    public class ContestController : BaseAdminController
    {
        public ContestController(IPhotoContestsData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = this.Data.Contests
                .All()
                .ProjectTo<ContestViewModel>();

            return this.Json(data.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult ReadPictures(int Id, [DataSourceRequest] DataSourceRequest request)
        {
            var data = this.Data.Pictures
                .All().Where(x => x.ContestId == Id)
                .ProjectTo<PictureViewModel>();

            return this.Json(data.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DeletePicture([DataSourceRequest] DataSourceRequest request, PictureViewModel model)
        {
            this.Data.Pictures.Remove(model.Id);
            var comments = this.Data.Comments.All().Where(x => x.PictureId == model.Id).ToList();
            foreach (var comment in comments)
            {
                this.Data.Comments.Remove(comment.Id);
            }
            this.Data.SaveChanges();

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ContestViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var contest = Mapper.Map<Contest>(model);
                this.Data.Contests.Add(contest);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ContestViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var contest = this.Data.Contests.Find(model.Id);
                contest.Title = model.Title;
                contest.Description = model.Description;
                contest.DateCreated = model.DateCreated;
                contest.DateEnded = model.DateEnded;
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult UpdatePicture([DataSourceRequest] DataSourceRequest request, PictureViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var pic = this.Data.Pictures.Find(model.Id);
                pic.Title = model.Title;
                pic.Description = model.Description;
                pic.ContestId = model.ContestId;
                pic.VoteCount = model.Vote;
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ContestViewModel model)
        {
            this.Data.Contests.Remove(model.Id);
            this.Data.SaveChanges();

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}