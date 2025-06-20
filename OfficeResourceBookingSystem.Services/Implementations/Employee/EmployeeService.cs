using OfficeResourceBookingSystem.Repository.Interfaces.Employee;
using OfficeResourceBookingSystem.Services.DTOs.Employee;
using OfficeResourceBookingSystem.Services.Helpers;
using OfficeResourceBookingSystem.Services.Interfaces.Employe;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Services.Implementations.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public async Task<GetEmployeeResponse> GetByIdAsync(int employeeId)
        {
            var employee = await employeeRepository.RetrieveAsync(employeeId);

            return new GetEmployeeResponse
            {
                EmployeeId = employeeId,
                FullName = employee.FullName,
                EmailAddress = employee.EmailAddress
            };
        }

        public async Task<GetAllEmployeesResponse> GetAllAsync()
        {
            var employees = await employeeRepository.RetrieveCollectionAsync(new EmployeeFilter()).ToListAsync();

            var allEmployeesResponse = new GetAllEmployeesResponse
            {
                Employees = employees.Select(MapToEmployeeInfo).ToList(),
                TotalCount = employees.Count
            };

            return allEmployeesResponse;
        }

        public async Task<UpdateEmployeeResponse> UpdateFullNameAsync(UpdateFullNameRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NewFullName))
                {
                    throw new ValidationException("Full name cannot be empty.");
                }
                   
                var update = new EmployeeUpdate
                {
                    FullName = new SqlString(request.NewFullName)
                };

                var success = await employeeRepository.UpdateAsync(request.EmployeeId, update);

                return new UpdateEmployeeResponse
                {
                    Success = success,
                    UpdatedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new UpdateEmployeeResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    UpdatedAt = DateTime.Now
                };
            }
        }

        public async Task<UpdateEmployeeResponse> UpdateEmailAsync(UpdateEmailRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NewEmail))
                {
                    throw new ValidationException("Email address cannot be empty.");
                }
                    
                var update = new EmployeeUpdate
                {
                    EmailAddress = new SqlString(request.NewEmail)
                };

                var success = await employeeRepository.UpdateAsync(request.EmployeeId, update);

                return new UpdateEmployeeResponse
                {
                    Success = success,
                    UpdatedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new UpdateEmployeeResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    UpdatedAt = DateTime.Now
                };
            }
        }

        private EmployeeInfo MapToEmployeeInfo(Models.Employee employee)
        {
            return new EmployeeInfo
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                EmailAddress = employee.EmailAddress
            };
        }
    }
}