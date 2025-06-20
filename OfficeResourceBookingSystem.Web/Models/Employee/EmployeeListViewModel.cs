namespace OfficeResourceBookingSystem.Web.Models.Employee
{
    public class EmployeeListViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }

        public int TotalCount { get; set; }
    }

    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }
    }
}
