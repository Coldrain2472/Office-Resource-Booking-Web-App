using OfficeResourceBookingSystem.Repository.Interfaces.Employee;
using OfficeResourceBookingSystem.Services.DTOs.Authentication;
using OfficeResourceBookingSystem.Services.Helpers;
using OfficeResourceBookingSystem.Services.Interfaces.Authentication;
using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository employeeRepository;

        public AuthenticationService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.EmailAddress) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Email address and password are required"
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new EmployeeFilter { EmailAddress = new SqlString(request.EmailAddress) };

            var employees = await employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var employee = employees.SingleOrDefault(); 

            if (employee == null || employee.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid email address or password"
                };
            }

            return new LoginResponse
            {
                Success = true,
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                EmailAddress = employee.EmailAddress
            };
        }
    }
}
