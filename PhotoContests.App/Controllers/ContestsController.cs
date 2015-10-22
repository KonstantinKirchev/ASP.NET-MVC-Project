using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoContests.App.Models.BindingModels;
using PhotoContests.App.Models.ViewModels;
using PhotoContests.Data;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestsData data)
            : base(data)
        {
        }

        public ContestsController(IPhotoContestsData data, User user)
            : base(data, user)
        {
        }

        // GET: Contests
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.DateCreated)
                .Where(c => c.IsClosed == false && c.ContestOwner.Id == userId);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
        }

        // GET: Contests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contest = this.Data.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }

            var contestViewModel = Mapper.Map<Contest, ContestDetailViewModel>(contest);

            return View(contestViewModel);
        }

        // GET: Contests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContestBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);

                var contest = new Contest()
                {
                    Title = model.Title,
                    Description = model.Description,
                    DateCreated = DateTime.Now,
                    DateEnded = model.DateEnded,
                    RewardStrategy = model.RewardStrategy,
                    VotingStrategy = model.VotingStrategy,
                    ParticipationStrategy = model.ParticipationStrategy,
                    DeadlineStrategy = model.DeadlineStrategy,
                    ContestOwnerId = userId
                };

                this.Data.Contests.Add(contest);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Contests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = this.Data.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContestOwnerId = new SelectList(this.Data.Users.All(), "Id", "FirstName", contest.ContestOwnerId);
            return View(contest);
        }

        // POST: Contests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,DateCreated,DateEnded,RewardStrategy,VotingStrategy,ParticipationStrategy,DeadlineStrategy,ContestOwnerId,IsClosed")] Contest contest)
        {
            if (ModelState.IsValid)
            {
                var foundContest = this.Data.Contests.Find(contest.Id);
                this.Data.Contests.Update(foundContest);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        // GET: Contests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = this.Data.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contest contest = this.Data.Contests.Find(id);
            this.Data.Contests.Remove(contest);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
