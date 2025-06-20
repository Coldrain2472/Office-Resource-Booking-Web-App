namespace OfficeResourceBookingSystem.Services.DTOs.Employee
{
    public class UpdateFullNameRequest
    {
        public int EmployeeId { get; set; }

        public string NewFullName { get; set; }
    }
}
