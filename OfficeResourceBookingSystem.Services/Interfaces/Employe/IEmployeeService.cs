using OfficeResourceBookingSystem.Services.DTOs.Employee;

namespace OfficeResourceBookingSystem.Services.Interfaces.Employe
{
    public interface IEmployeeService
    {
        Task<GetEmployeeResponse> GetByIdAsync(int employeeId);

        Task<GetAllEmployeesResponse> GetAllAsync();

        Task<UpdateEmployeeResponse> UpdateFullNameAsync(UpdateFullNameRequest request);

        Task<UpdateEmployeeResponse> UpdateEmailAsync(UpdateEmailRequest request);
    }
}
