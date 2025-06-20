using Microsoft.AspNetCore.Mvc;
using OfficeResourceBookingSystem.Services.Interfaces.Employe;
using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
using OfficeResourceBookingSystem.Services.Interfaces.Resource;
using OfficeResourceBookingSystem.Web.Attributes;
using OfficeResourceBookingSystem.Web.Helpers;
using OfficeResourceBookingSystem.Web.Models;
using OfficeResourceBookingSystem.Web.Models.Reservation;
using System.Diagnostics;

namespace OfficeResourceBookingSystem.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IEmployeeService employeeService;
        private readonly IResourceService resourceService;

        public HomeController(IReservationService _reservationService, IEmployeeService _employeeService, IResourceService _resourceService)
        {
            reservationService = _reservationService;
            employeeService = _employeeService;
            resourceService = _resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);

            var inactiveReservations = await reservationService.GetInactiveReservationsAsync();

            var viewModel = new InactiveReservationsListViewModel
            {
                InactiveReservations = new List<ReservationViewModel>()
            };

            foreach (var reservation in inactiveReservations.Reservations)
            {
                viewModel.InactiveReservations.Add(new ReservationViewModel
                {
                    ReservationId = reservation.ReservationId,
                    CreatedAt = reservation.CreatedAt,
                    IsActive = reservation.IsActive,
                    StartTime = reservation.StartTime,
                    CreatedByEmployeeId = reservation.CreatorId,
                    CreatedByName = reservation.CreatedByName,
                    EndTime = reservation.EndTime,
                    ParticipantsCount = reservation.ParticipantsCount,
                    Purpose = reservation.Purpose,
                    ReservedResourceId = reservation.ResourceId,
                    ReservedResourceName = reservation.ResourceName
                });
            }

            viewModel.TotalCount = viewModel.InactiveReservations.Count;
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
