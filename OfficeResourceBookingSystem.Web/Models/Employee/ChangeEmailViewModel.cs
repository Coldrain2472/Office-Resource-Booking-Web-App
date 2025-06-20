using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Web.Models.Employee
{
    public class ChangeEmailViewModel
    {
        public int EmployeeId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
