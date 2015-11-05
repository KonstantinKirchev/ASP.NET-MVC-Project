namespace PhotoContests.App.Areas.Admin.Models.ViewModels
{
    using Common.Mappings;
    using PhotoContests.Models;
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel : IMapFrom<User>,IMapTo<User>
    {
        [Required(ErrorMessage = "ID is required!!!")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email address is required!!!")]
        [EmailAddress(ErrorMessage = "Please valid email format!!!")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Please enter valid phone format!!!")]
        public string PhoneNumber { get; set; }
    }
}