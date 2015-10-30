using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoContests.App.Models.ViewModels;

namespace PhotoContests.App.Hubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNet.SignalR;

    using PhotoContests.Data;
    using PhotoContests.Models;

    public class VoteHub : Hub
    {
        private static PhotoContestsDbContext db = new PhotoContestsDbContext();
        private static IHubContext hubVoteContext = GlobalHost.ConnectionManager.GetHubContext<VoteHub>();

        public void Vote(int id)
        {
            // AddVote tabulates the vote         
            var pictureViewModel = AddVote(id);

            // Clients.All.updateVoteResults notifies all clients that someone has voted and the page updates itself to relect that
            hubVoteContext.Clients.All.updateVoteResults(id, pictureViewModel);
            //Clients.All.updateVoteResults(id, votes);
        }
        private PictureViewModel AddVote(int id)
        {
            if (Context.User == null)
            {
                return null;
            }
            var user = db.Users.Find(Context.User.Identity.GetUserId());
            if (user == null)
            {
                return null;
            }

            var item = db.Pictures.Find(id);
            if (item.Voters.Contains(user))
            {
                return Mapper.Map<Picture, PictureViewModel>(item);
            }
            item.VoteCount++;
            item.Voters.Add(user);
            db.SaveChanges();

            return Mapper.Map<Picture, PictureViewModel>(item);
        }
    }
}