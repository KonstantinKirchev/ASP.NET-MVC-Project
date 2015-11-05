namespace PhotoContests.App.Models.BindingModels
{
    using System.Web;

    public class AddPictureBindingModel
    {
        public HttpPostedFileBase Picture { get; set; }
        public int ContestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}