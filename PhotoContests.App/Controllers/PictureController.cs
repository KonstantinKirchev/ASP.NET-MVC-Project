using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;
using System.IO;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;

namespace PhotoContests.App.Controllers
{
    using Models.ViewModels;
    using System.Collections.Generic;
    using Google.Apis.Drive.v2.Data;
    using Models.BindingModels;

    public class PictureController : BaseController
    {
        public PictureController(IPhotoContestsData data) : base(data)
        {
        }

        public ActionResult UploadPicture(AddPictureBindingModel model)
        {
            var contest = this.Data.Contests.Find(model.ContestId);
            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            var url = Request.UrlReferrer.AbsolutePath;

            if (contest.ParticipationStrategy == ParticipationStrategy.Closed)
            {
                if (!contest.SelectedUsersForParticipation.Contains(user))
                {
                    //this.TempData["partisipation-message"] = "You don't have permission to participate in this contest!";
                    return this.Json("Not", JsonRequestBehavior.AllowGet);
                }
            }

            if (contest.DeadlineStrategy == DeadlineStrategy.ByNumberOfParticipants)
            {
                var pictures = this.Data.Pictures
                    .All()
                    .Where(p => p.ContestId == contest.Id)
                    .Select(p => new
                    {
                        OwnerId = p.OwnerId
                    })
                    .ToList();

                var participantsIds = new HashSet<string>();

                foreach (var picture in pictures)
                {
                    participantsIds.Add(picture.OwnerId);
                }

                if (participantsIds.Count >= contest.MaxParticipants && !participantsIds.Contains(userId))
                {
                    //this.TempData["partisipation-message"] = "You don't have permission to participate in this contest!";
                    return this.Json("Max", JsonRequestBehavior.AllowGet);
                }
            }

            if (model.Picture != null && model.Picture.ContentLength > 0)
            {
                Guid id = Guid.NewGuid();
                string fileName = id.ToString();
                string fileExtension = model.Picture.FileName.Split('.').Last();

                MemoryStream target = new MemoryStream();
                model.Picture.InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                UserCredential credential;
                using (var filestream = new FileStream(Server.MapPath("~/client_secret.json"), FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(filestream).Secrets,
                        new[] { DriveService.Scope.Drive },
                        "yunay07abv.bg",
                        CancellationToken.None,
                        new FileDataStore(Server.MapPath("~/Contests"))).Result;
                }

                // Create the service.
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "My Project",
                });

                File body = new File();
                body.Title = fileName;
                body.Shared = true;
                body.Shareable = true;
                body.Parents = new List<ParentReference>{ new ParentReference() {Id = "0B3FfQDv4R4vyMkZ0Z2ZkMC0tWjg"} };
                body.Description = "Image";
                body.MimeType = "image/jpeg";
                body.FileExtension = fileExtension;
                MemoryStream stream = new MemoryStream(data);
                FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
                request.Upload();

                File file = request.ResponseBody;
                var newPic = new Picture();
             
                newPic.ContestId = model.ContestId;
                newPic.PictureUrl = "https://drive.google.com/uc?id=" + file.Id;
                newPic.VoteCount = 0;
                newPic.OwnerId = this.User.Identity.GetUserId();
                newPic.Description = model.Description;
                newPic.Title = model.Title;

                this.Data.Pictures.Add(newPic);
                this.Data.SaveChanges();

                PictureViewModel lastPicture = new PictureViewModel();

                lastPicture.CommentsCount = 0;
                lastPicture.Id = newPic.Id;
                lastPicture.PictureUrl = "https://drive.google.com/uc?id=" + file.Id;
                lastPicture.Comments = new List<CommentViewModel>();
                lastPicture.Owner = this.UserProfile.UserName;
                lastPicture.Title = model.Title;
                lastPicture.Description = model.Description;

                return PartialView("~/Views/Pictures/_ShowPicture.cshtml", lastPicture);
            }

            return View("/");
        }
    }
}