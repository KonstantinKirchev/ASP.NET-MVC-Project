using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glimpse.Mvc.AlternateType;
using Microsoft.Ajax.Utilities;

namespace PhotoContests.App.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public int PictureId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}