using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoContests.App.Models.BindingModels;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    public class PrizesController : BaseController
    {
        public PrizesController(IPhotoContestsData data)
            : base(data)
        {
        }

        public PrizesController(IPhotoContestsData data, User user)
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrizeBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var prize = Mapper.Map<Prize>(model);
                var userId = this.User.Identity.GetUserId();
                prize.OwnerId = userId;

                this.Data.Prizes.Add(prize);
                this.Data.SaveChanges();

                ViewBag.Success = "Success";
            }
            else
            {
                ViewBag.Success = "Failed";
            }

            return this.View();
        }
    }
}