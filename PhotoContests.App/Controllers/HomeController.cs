<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using PagedList;
=======
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
using PhotoContests.App.Models.ViewModels;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestsData data)
            : base(data)
        {
        }

        public HomeController(IPhotoContestsData data, User user)
            : base(data, user)
        {
        }

<<<<<<< HEAD
        public ActionResult Index(int? page)
        {
            var contests = this.Data.Contests.All()
                .Where(c => c.DateEnded > DateTime.Now || c.DateEnded == null)
                .OrderByDescending(c => c.DateCreated)
                .ProjectTo<ContestViewModel>()
                .ToPagedList(page ?? 1, 9);

            return View(contests);
=======
        public ActionResult Index()
        {
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.DateCreated)
                .Where(c => c.IsClosed == false);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(contests);
            return View(contestModels);
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
        }
    }
}