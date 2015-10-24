using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoContests.App.Models.BindingModels;
using PhotoContests.App.Models.ViewModels;
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

        // GET: Closed Contests
        public ActionResult ClosedContests()
        {
            var userId = User.Identity.GetUserId();
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.DateCreated)
                .Where(c => c.IsClosed == true && c.ContestOwner.Id == userId);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
        }

        // GET: Past Contests
        public ActionResult PastContests()
        {
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.DateCreated)
                .Where(c => c.IsClosed == true);

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
        public ActionResult TempCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TempCreate(ContestBindingModel model)
        {
            this.Session["Title"] = model.Title;
            this.Session["Description"] = model.Description;
            this.Session["RewardStrategy"] = model.RewardStrategy;
            this.Session["VotingStrategy"] = model.VotingStrategy;
            this.Session["ParticipationStrategy"] = model.ParticipationStrategy;
            this.Session["DeadlineStrategy"] = model.DeadlineStrategy;
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var currentUser = User.Identity.GetUserName();
            var users = this.Data.Users.All()
                .Where(u => u.UserName != currentUser)
                .Select(u => new
                {
                    Username = u.UserName
                })
                .ToList();

            ViewBag.Users = new MultiSelectList(users, "Username");

            return View();
        }
        
        // POST: Contests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContestFinalBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);

                string title = this.Session["Title"].ToString();
                string description = this.Session["Description"].ToString();
                string rewardStrategy = this.Session["RewardStrategy"].ToString();
                string votingStrategy = this.Session["VotingStrategy"].ToString();
                string participationStrategy = this.Session["ParticipationStrategy"].ToString();
                string deadlineStrategy = this.Session["DeadlineStrategy"].ToString();

                var contest = new Contest()
                {
                    Title = title,
                    Description = description,
                    DateCreated = DateTime.Now,
                    DateEnded = model.DateEnded,
                    RewardStrategy = rewardStrategy == RewardStrategy.SingleUser.ToString() ? RewardStrategy.SingleUser : RewardStrategy.TopNUsers,
                    VotingStrategy = votingStrategy == VotingStrategy.Open.ToString() ? VotingStrategy.Open : VotingStrategy.Closed,
                    ParticipationStrategy = participationStrategy == ParticipationStrategy.Open.ToString() ? ParticipationStrategy.Open : ParticipationStrategy.Closed,
                    DeadlineStrategy = deadlineStrategy == DeadlineStrategy.ByTime.ToString() ? DeadlineStrategy.ByTime : DeadlineStrategy.ByNumberOfParticipants,
                    ContestOwnerId = userId,
                    Winners = model.WinnersCount != null ? model.WinnersCount : null,
                    MaxParticipants = model.NumberOfParticipation != null ? model.NumberOfParticipation : null
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

        // GET: Contests/Dismiss/5
        public ActionResult Dismiss(int? id)
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

        // POST: Contests/Dismiss/5
        [HttpPost, ActionName("Dismiss")]
        [ValidateAntiForgeryToken]
        public ActionResult DismissConfirmed(int id)
        {
            var contest = this.Data.Contests.Find(id);
            contest.IsClosed = true;
            contest.DateEnded = DateTime.Now;
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contests/Finalize/5
        public ActionResult Finalize(int? id)
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

        // POST: Contests/Finalize/5
        [HttpPost, ActionName("Finalize")]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizeConfirmed(int id)
        {
            var contest = this.Data.Contests.Find(id);
            contest.IsClosed = true;
            contest.DateEnded = DateTime.Now;
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contests/Delete/5
        public ActionResult Delete(int? id)
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

            return View(contest);
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contest = this.Data.Contests.Find(id);
            this.Data.Contests.Remove(contest);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
