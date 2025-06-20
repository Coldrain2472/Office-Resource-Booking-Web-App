namespace OfficeResourceBookingSystem.Services.DTOs.Employee
{
    public class UpdateEmailRequest
    {
        public int EmployeeId { get; set; }

        public string NewEmail { get; set; }
    }
}
