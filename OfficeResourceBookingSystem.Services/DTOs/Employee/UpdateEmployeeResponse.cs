namespace OfficeResourceBookingSystem.Services.DTOs.Employee
{
    public class UpdateEmployeeResponse
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime UpdatedAt { get; set; } 
    }
}
