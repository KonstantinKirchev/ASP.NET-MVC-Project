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
<<<<<<< HEAD
                .Where(c => (c.DateEnded > DateTime.Now || c.DateEnded == null) && c.ContestOwner.Id == userId);
=======
                .Where(c => c.IsClosed == false && c.ContestOwner.Id == userId);
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
        }

        // GET: Closed Contests
        public ActionResult ClosedContests()
        {
            var userId = User.Identity.GetUserId();
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.DateCreated)
<<<<<<< HEAD
                .Where(c => c.DateEnded < DateTime.Now && c.ContestOwner.Id == userId);
=======
                .Where(c => c.IsClosed == true && c.ContestOwner.Id == userId);
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
        }

        // GET: Past Contests
        public ActionResult PastContests()
        {
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.DateCreated)
<<<<<<< HEAD
                .Where(c => c.DateEnded < DateTime.Now);
=======
                .Where(c => c.IsClosed == true);
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
        }

        // GET: Contests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
<<<<<<< HEAD
                return RedirectToAction("NotFound","Error");
=======
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
            }
            var contest = this.Data.Contests.Find(id);
            if (contest == null)
            {
<<<<<<< HEAD
                return RedirectToAction("NotFound", "Error");
            }

            if (contest.DateEnded < DateTime.Now)
            {
                this.FinalizeConfirmed(contest.Id);
            }

            var contestViewModel = Mapper.Map<Contest, ContestDetailViewModel>(contest);
            //var contestViewModel = new ContestDetailViewModel()
            //{
            //    Id = contest.Id,
            //    Title = contest.Title,
            //    DateCreated = contest.DateCreated,
            //    DateEnded = contest.DateEnded,
            //    Description = contest.Description,
            //    Owner = contest.ContestOwner.UserName,                
            //};

            //foreach (var contestPicture in contest.ContestPictures)
            //{
                
            //}

            //IEnumerable<WinnerPrizeViewModel> winners = null;

            //foreach (var contestWinner in contest.Prizes)
            //{
            //    string prize = contestWinner.Name;
            //    string winner = contestWinner.Winner.UserName;
            //    var currentWinner = new WinnerPrizeViewModel()
            //    {
            //        Name = prize,
            //        WinnerUsername = winner
            //    };
            //}

            //contestViewModel.Winners = winners;
=======
                return HttpNotFound();
            }

            var contestViewModel = Mapper.Map<Contest, ContestDetailViewModel>(contest);
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3

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
<<<<<<< HEAD
            this.LoadPrizes();
            this.LoadUsers();
            return View();
        }

        private void LoadUsers()
        {
            string userId = this.User.Identity.GetUserId();
            this.ViewBag.UserIds = this.Data.Users.All()
                .Where(u => u.Id != userId)
                .Select(u => new SelectListItem()
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                });
        }

        private void LoadPrizes()
        {
            var userId = this.User.Identity.GetUserId();
            this.ViewBag.PrizeIds = this.Data.Prizes.All()
            .Where(u => u.OwnerId == userId && u.ContestId == null)
            .Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        }

        public ActionResult GetPartisipators()
        {
            this.LoadUsers();
            return this.PartialView("_AddPartisipatorPartialView");
        }

        public ActionResult GetPrizes()
        {
            this.LoadPrizes();
            return this.PartialView("_CreatePrizePartial");
        }

        public ActionResult GetVoters()
        {
            this.LoadUsers();
            return this.PartialView("_AddVoterPartialView");
        }

=======
          
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
        
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
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
<<<<<<< HEAD

                if (contest.ParticipationStrategy == ParticipationStrategy.Closed)
                {
                    foreach (var partisipatorId in model.PartisipatorIds)
                    {
                        var partisipator = this.Data.Users.Find(partisipatorId);
                        contest.SelectedUsersForParticipation.Add(partisipator);
                        var notification = new Notification
                        {
                            Content = "You are invited to participate in " + contest.Title + " contest.",
                            UserId = userId
                        };
                        partisipator.Notifications.Add(notification);

                    }
                }

                if (contest.VotingStrategy == VotingStrategy.Closed)
                {
                    foreach (var voterId in model.VoterIds)
                    {
                        var voter = this.Data.Users.Find(voterId);
                        contest.InvitedUsersForVoting.Add(voter);
                        var notification = new Notification()
                        {
                            Content = "You are invited for voting in " + contest.Title + " contest.",
                            UserId = userId
                        };
                        voter.Notifications.Add(notification);

                    }
                }

                if (model.PrizeIds.Any())
                {
                    foreach (var prizeId in model.PrizeIds)
                    {
                        var prize = this.Data.Prizes.Find(prizeId);
                        prize.ContestId = contest.Id;
                    }
                }
                
                this.Data.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            this.LoadPrizes();
            this.LoadUsers();

=======
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
            return View();
        }

        // GET: Contests/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = this.User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contest contest = this.Data.Contests.Find(id);

            if (contest == null)
            {
                return HttpNotFound();
            }

            if (userId != contest.ContestOwner.Id)
            {
                return RedirectToAction("Index", "Home");
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
            var userId = this.User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = this.Data.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            if (userId != contest.ContestOwner.Id)
            {
                return RedirectToAction("Index","Home");
            }
<<<<<<< HEAD
            
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
 
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
<<<<<<< HEAD
            var prizes = contest.Prizes.ToList();

            var picturesMostVoted = contest.ContestPictures
                .OrderByDescending(p => p.VoteCount)
                .Take(prizes.Count)
                .ToList();

            for (int i = 0; i < picturesMostVoted.Count; i++)
            {
                prizes[i].WinnerId = picturesMostVoted[i].OwnerId;
                prizes[i].WinnerPosition = i + 1;
            }

            contest.DateEnded = DateTime.Now;
            this.Data.SaveChanges();
            return RedirectToAction("Details", new {id = contest.Id});
=======
            contest.IsClosed = true;
            contest.DateEnded = DateTime.Now;
            this.Data.SaveChanges();
            return RedirectToAction("Index");
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
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
