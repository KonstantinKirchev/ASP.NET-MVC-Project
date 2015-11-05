using System.Linq;

namespace PhotoContests.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Kendo.Mvc.UI;
    using Models.ViewModels;
    using AutoMapper;
    using Kendo.Mvc.Extensions;
    using PhotoContests.Models;
    using AutoMapper.QueryableExtensions;

    public class UserController : BaseAdminController
    {
        public UserController(IPhotoContestsData data)
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
            var adminId = this.UserProfile.Id;
            var data = this.Data.Users
                .All()
                .Where(x => x.Id != adminId)
                .ProjectTo<UserViewModel>();

            return this.Json(data.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, UserViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var user = Mapper.Map<User>(model);
                this.Data.Users.Add(user);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, UserViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var user = this.Data.Users.Find(model.Id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, UserViewModel model)
        {
            this.Data.Users.Remove(model.Id);
            this.Data.SaveChanges();

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}