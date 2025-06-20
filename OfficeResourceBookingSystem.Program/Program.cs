//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using OfficeResourceBookingSystem.Repository;
//using OfficeResourceBookingSystem.Repository.Implementations.Employee;
//using OfficeResourceBookingSystem.Repository.Implementations.Reservation;
//using OfficeResourceBookingSystem.Repository.Implementations.Resource;
//using OfficeResourceBookingSystem.Repository.Interfaces.Employee;
//using OfficeResourceBookingSystem.Repository.Interfaces.Reservation;
//using OfficeResourceBookingSystem.Repository.Interfaces.Resource;
//using OfficeResourceBookingSystem.Services.DTOs.Authentication;
//using OfficeResourceBookingSystem.Services.DTOs.Employee;
//using OfficeResourceBookingSystem.Services.Implementations.Authentication;
//using OfficeResourceBookingSystem.Services.Implementations.Employee;
//using OfficeResourceBookingSystem.Services.Implementations.Reservation;
//using OfficeResourceBookingSystem.Services.Implementations.Resource;
//using OfficeResourceBookingSystem.Services.Interfaces.Authentication;
//using OfficeResourceBookingSystem.Services.Interfaces.Employe;
//using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
//using OfficeResourceBookingSystem.Services.Interfaces.Resource;

//namespace OfficeResourceBookingSystem.Program
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            // login with email + password!
//            // password is Password123!
//            // example: miranda.kerr@gmail.com

//            var configuration = new ConfigurationBuilder()
//           .SetBasePath(Directory.GetCurrentDirectory())
//           .AddJsonFile("appsettings.json")
//           .Build();

//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            ConnectionFactory.Initialize(connectionString);
//            var services = new ServiceCollection();

//            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//            services.AddScoped<IResourceRepository, ResourceRepository>();
//            services.AddScoped<IReservationRepository, ReservationRepository>();

//            services.AddScoped<IEmployeeService, EmployeeService>();
//            services.AddScoped<IAuthenticationService, AuthenticationService>();
//            services.AddScoped<IResourceService, ResourceService>();
//            services.AddScoped<IReservationService, ReservationService>();

//            var serviceProvider = services.BuildServiceProvider();

//            try
//            {
//                var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
//                var employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
//                var resourceService = serviceProvider.GetRequiredService<IResourceService>();
//                var reservationService = serviceProvider.GetRequiredService<IReservationService>();

//                Console.WriteLine("Login as an employee:");
//                Console.Write("Email address: ");
//                var emailAddress = Console.ReadLine();
//                Console.Write("Password: ");
//                var password = Console.ReadLine();

//                var loginResult = await authenticationService.LoginAsync(new LoginRequest
//                {
//                    EmailAddress = emailAddress,
//                    Password = password
//                });

//                if (!loginResult.Success)
//                {
//                    Console.WriteLine($"Login failed: {loginResult.ErrorMessage}");
//                    return;
//                }

//                Console.WriteLine($"Logged in as: {loginResult.FullName}");

//                var employees = await employeeService.GetAllAsync();
//                Console.WriteLine("\nEmployees:");
//                foreach (var employee in employees.Employees)
//                {
//                    Console.WriteLine($"Id: {employee.EmployeeId}, Name: {employee.FullName}, Email: {employee.EmailAddress}");
//                }

//                var resources = await resourceService.GetAllAsync();
//                Console.WriteLine("\nResources:");
//                foreach (var resource in resources.Resources)
//                {
//                    Console.WriteLine($"Id: {resource.ResourceId}, Name: {resource.Name}, Type: {resource.Type}, Description: {resource.Description ?? null}");
//                }

//            }
//            catch (Exception ex)
//            {

//                Console.WriteLine($"\nError occurred: {ex.Message}");
//            }
            
//        }
//    }
//}
