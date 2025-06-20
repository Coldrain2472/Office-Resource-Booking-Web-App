using Microsoft.AspNetCore.Mvc;
using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
using OfficeResourceBookingSystem.Services.Interfaces.Resource;
using OfficeResourceBookingSystem.Web.Attributes;
using OfficeResourceBookingSystem.Web.Models.Resource;

namespace OfficeResourceBookingSystem.Web.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;
        private readonly IReservationService reservationService;

        public ResourceController(IResourceService _resourceService, IReservationService _reservationService)
        {
            resourceService = _resourceService;
            reservationService = _reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allResources = await resourceService.GetAllAsync();
            var viewModel = new ResourceListViewModel
            {
                Resource = allResources.Resources.Select(r => new ResourceViewModel
                {
                    ResourceId = r.ResourceId,
                    Name = r.Name,
                    Type = r.Type,
                    Description = r.Description ?? string.Empty,
                    IsAvailable = r.IsAvailable
                })
                .ToList(),
                TotalCount = allResources.TotalCount
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MostUsed()
        {
            var response = await resourceService.GetMostUsedResources();
            var viewModel = new MostUsedResourceViewModel
            {
                ResourceStatistics = response.ResourceStatistics
                    .OrderByDescending(kvp => kvp.Value)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UsageReport()
        {
            var response = await resourceService.GetResourceUsageReport();
            var viewModel = new ResourceUsageReportViewModel
            {
                ResourceUsages = response.ResourceUsages
                    .OrderByDescending(kvp => kvp.Value)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            };

            return View(viewModel); 
        }

        [HttpGet]
        public async Task<IActionResult> ReservationHistory(int resourceId)
        {
            var resource = await resourceService.GetByIdAsync(resourceId);
            if (resource == null)
            {
                return NotFound();
            }

            var history = await reservationService.GetHistoryOfReservationsPerResource(resourceId);

            var viewModel = new ResourceReservationHistoryViewModel
            {
                ResourceName = resource.Name,
                Reservations = history.Reservations,
                TotalCount = history.TotalCount
            };

            return View(viewModel);
        }
    }
}
