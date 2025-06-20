namespace OfficeResourceBookingSystem.Services.DTOs.Employee
{
    public class GetAllEmployeesResponse
    {
        public List<EmployeeInfo> Employees { get; set; }

        public int TotalCount { get; set; } 
    }
}
