using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Web.Models.Employee
{
    public class ChangeNameViewModel
    {
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
    }
}
