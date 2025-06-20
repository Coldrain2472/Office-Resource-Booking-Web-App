using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Web.Models.Account
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
