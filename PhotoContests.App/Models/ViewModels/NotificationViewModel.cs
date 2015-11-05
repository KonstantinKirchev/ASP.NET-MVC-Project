using System;
using PhotoContests.Common.Mappings;
using PhotoContests.Models;

namespace PhotoContests.App.Models.ViewModels
{
    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsViewed { get; set; }
    }
}