namespace OfficeResourceBookingSystem.Services.DTOs.Authentication
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public int? EmployeeId { get; set; }

        public string? FullName { get; set; }

        public string? ErrorMessage { get; set; }

        public string? EmailAddress { get; set; }
    }
}
