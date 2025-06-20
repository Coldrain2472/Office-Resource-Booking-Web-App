using Microsoft.AspNetCore.Mvc;
using OfficeResourceBookingSystem.Repository.Implementations.Employee;
using OfficeResourceBookingSystem.Services.DTOs.Employee;
using OfficeResourceBookingSystem.Services.Interfaces.Employe;
using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
using OfficeResourceBookingSystem.Web.Attributes;
using OfficeResourceBookingSystem.Web.Helpers;
using OfficeResourceBookingSystem.Web.Models.Employee;

namespace OfficeResourceBookingSystem.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IReservationService reservationService;

        public EmployeeController(IEmployeeService _employeeService, IReservationService _reservationService)
        {
            employeeService = _employeeService;
            reservationService = _reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allEmployees = await employeeService.GetAllAsync();
            var viewModel = new EmployeeListViewModel
            {
                Employees = allEmployees.Employees.Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress
                })
                .ToList(),
                TotalCount = allEmployees.TotalCount
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeName(int employeeId)
        {
            var employee = await employeeService.GetByIdAsync(employeeId);

            var viewModel = new ChangeNameViewModel
            {
                EmployeeId = employeeId,
                Name = employee.FullName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeName(ChangeNameViewModel model)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);

            var request = new UpdateFullNameRequest
            {
                NewFullName = model.Name,
                EmployeeId = model.EmployeeId
            };

            var response = await employeeService.UpdateFullNameAsync(request);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEmail(int employeeId)
        {
            var employee = await employeeService.GetByIdAsync(employeeId);

            var viewModel = new ChangeEmailViewModel
            {
                EmployeeId = employeeId,
                Email = employee.EmailAddress
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUserId = IdHelper.GetUserId(HttpContext);
            if (currentUserId != model.EmployeeId)
            {
                return RedirectToAction(nameof(Index));
            }

            var employee = await employeeService.GetByIdAsync(model.EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var request = new UpdateEmailRequest
            {
                NewEmail = model.Email,
                EmployeeId = model.EmployeeId
            };
            var response = await employeeService.UpdateEmailAsync(request);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ReservationHistory(int employeeId)
        {
            var employee = await employeeService.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var historyResponse = await reservationService.GetHistoryOfReservationsPerEmployee(employeeId);

            var viewModel = new ReservationHistoryPerEmployeeViewModel
            {
                EmployeeName = employee.FullName,
                Reservations = historyResponse.Reservations,
                TotalCount = historyResponse.TotalCount
            };

            return View(viewModel);
        }
    }
}
