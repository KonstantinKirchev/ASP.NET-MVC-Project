using System.Collections.Specialized;
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

        public void Vote(int id, int oldVoteCount, string connectionId)
        {
            string name = Context.User.Identity.Name;

            // AddVote tabulates the vote         
            var pictureViewModel = AddVote(id);

            // Clients.All.updateVoteResults notifies all clients that someone has voted and the page updates itself to relect that
            if (pictureViewModel == null || 
                pictureViewModel.VoteCount == oldVoteCount)
            {
                hubVoteContext.Clients.Group(connectionId).updateVoteResults(id, pictureViewModel);
                return;
            }

            //hubVoteContext.Clients.Group(connectionId).updateVoteResults(id, pictureViewModel);
            hubVoteContext.Clients.All.updateVoteResults(id, pictureViewModel);
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
<<<<<<< HEAD

            if (item.Contest.VotingStrategy == VotingStrategy.Closed)
            {
                if (!item.Contest.InvitedUsersForVoting.Contains(user))
                {
                    return Mapper.Map<Picture, PictureViewModel>(item);
                }
            }

=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
            item.VoteCount++;
            item.Voters.Add(user);
            db.SaveChanges();

            return Mapper.Map<Picture, PictureViewModel>(item);
        }

        public override Task OnConnected()
        {
            Groups.Add(Context.ConnectionId, Context.ConnectionId);

            return base.OnConnected();
        }
    }
}