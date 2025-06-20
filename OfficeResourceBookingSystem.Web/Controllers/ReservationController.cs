using Microsoft.AspNetCore.Mvc;
using OfficeResourceBookingSystem.Services.DTOs.Reservation;
using OfficeResourceBookingSystem.Services.Interfaces.Employe;
using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
using OfficeResourceBookingSystem.Services.Interfaces.Resource;
using OfficeResourceBookingSystem.Web.Attributes;
using OfficeResourceBookingSystem.Web.Models.Reservation;
using OfficeResourceBookingSystem.Web.Helpers;

namespace OfficeResourceBookingSystem.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IEmployeeService employeeService;
        private readonly IResourceService resourceService;

        public ReservationController(IReservationService _reservationService, IEmployeeService _employeeService, IResourceService _resourceService)
        {
            reservationService = _reservationService;
            employeeService = _employeeService;
            resourceService = _resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var reservations = await reservationService.GetActiveReservationsAsync();

            var viewModel = new ActiveReservationsListViewModel
            {
                ActiveReservations = new List<ReservationViewModel>()
            };

            foreach (var reservation in reservations.Reservations)
            {
                viewModel.ActiveReservations.Add(new ReservationViewModel
                {
                    ReservationId = reservation.ReservationId,
                    CreatedByName = reservation.CreatedByName,
                    ReservedResourceName = reservation.ResourceName,
                    IsActive = reservation.IsActive,
                    CreatedAt = reservation.CreatedAt,
                    EndTime = reservation.EndTime,
                    ParticipantsCount = reservation.ParticipantsCount,
                    Purpose = reservation.Purpose,
                    StartTime = reservation.StartTime,
                    CreatedByEmployeeId = reservation.CreatorId,
                    ReservedResourceId = reservation.ResourceId
                });
            }

            viewModel.TotalCount = viewModel.ActiveReservations.Count;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int resourceId)
        {
            var resource = await resourceService.GetByIdAsync(resourceId);

            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);

            var viewModel = new ReservationViewModel
            {
                ReservedResourceId = resource.ResourceId,
                ReservedResourceName = resource.Name,
                CreatedByName = employee.FullName,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationViewModel model)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);

            var request = new CreateReservationRequest
            {
                CreatorId = currentUserId,
                CreatedByName = employee.FullName,
                CreatedAt = DateTime.Now,
                ReservationId = model.ReservationId,
                IsActive = model.IsActive,
                EndTime = model.EndTime,
                ParticipantsCount = model.ParticipantsCount,
                Purpose = model.Purpose,
                ResourceId = model.ReservedResourceId,
                ResourceName = model.ReservedResourceName,
                StartTime = model.StartTime
            };

            var response = await reservationService.CreateReservationAsync(request);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> End(int reservationId)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);

            var request = new EndReservationRequest
            {
                CreatedByEmployeeId = currentUserId,
                ReservationId = reservationId
            };

            var response = await reservationService.EndReservationAsync(request);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int reservationId)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);
            var reservation = await reservationService.GetByIdAsync(reservationId);
            var viewModel = new EditReservationViewModel
            {
                EndTime = reservation.EndTime,
                IsActive = reservation.IsActive,
                ParticipantsCount = reservation.ParticipantsCount,
                Purpose = reservation.Purpose,
                ReservationId = reservationId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReservationViewModel model)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);
            var reservation = await reservationService.GetByIdAsync(model.ReservationId);

            var request = new UpdateReservationRequest
            {
                EndTime = model.EndTime,
                IsActive = model.IsActive,
                ParticipantsCount = model.ParticipantsCount,
                Purpose = model.Purpose
            };

            var response = await reservationService.UpdateAsync(reservation.ReservationId, request);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
