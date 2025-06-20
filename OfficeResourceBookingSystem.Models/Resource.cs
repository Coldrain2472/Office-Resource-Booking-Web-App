using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Resource name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Resource name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Type must be between 2 and 50 characters.")]
        public string Type { get; set; }

        [StringLength(200, MinimumLength = 0, ErrorMessage = "Description must be between 0 and 200 characters.")]
        public string? Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
