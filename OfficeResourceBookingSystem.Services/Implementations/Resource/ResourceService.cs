using OfficeResourceBookingSystem.Repository.Interfaces.Reservation;
using OfficeResourceBookingSystem.Repository.Interfaces.Resource;
using OfficeResourceBookingSystem.Services.DTOs.Resource;
using OfficeResourceBookingSystem.Services.Interfaces.Resource;

namespace OfficeResourceBookingSystem.Services.Implementations.Resource
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository resourceRepository;
        private readonly IReservationRepository reservationRepository;

        public ResourceService(IResourceRepository _resourceRepository, IReservationRepository _reservationRepository)
        {
            resourceRepository = _resourceRepository;
            reservationRepository = _reservationRepository;
        }

        public async Task<GetResourceResponse> GetByIdAsync(int resourceId)
        {
            var resource = await resourceRepository.RetrieveAsync(resourceId);

            var resourceInfo = MapToResourceInfo(resource);

            return new GetResourceResponse()
            {
                Description = resourceInfo.Description,
                ResourceId = resourceId,
                IsAvailable = resourceInfo.IsAvailable,
                Name = resourceInfo.Name,
                Type = resourceInfo.Type
            };
        }

        public async Task<GetAllResourcesResponse> GetAllAsync()
        {
            var resources = await resourceRepository.RetrieveCollectionAsync(new ResourceFilter()).ToListAsync();

            var allResourceResponse = new GetAllResourcesResponse
            {
                Resources = resources.Select(MapToResourceInfo).ToList(),
                TotalCount = resources.Count
            };

            return allResourceResponse;
        }

        public async Task<GetMostUsedResourcesResponse> GetMostUsedResources()
        {
            var reservations = await reservationRepository.RetrieveCollectionAsync(new ReservationFilter()).ToListAsync();
            var resources = await resourceRepository.RetrieveCollectionAsync(new ResourceFilter()).ToListAsync();
            var joined = from reservation in reservations
                         join resource in resources
                         on reservation.ResourceId equals resource.ResourceId
                         select resource.Type;

            var usage = joined
                .GroupBy(type => type)
                .ToDictionary(g => g.Key, g => g.Count());

            return new GetMostUsedResourcesResponse
            {
                ResourceStatistics = usage
            };

        }

        public async Task<GetResourceUsageReportResponse> GetResourceUsageReport()
        {
            var reservations = await reservationRepository.RetrieveCollectionAsync(new ReservationFilter()).ToListAsync();
            var resources = await resourceRepository.RetrieveCollectionAsync(new ResourceFilter()).ToListAsync();

            var usageByName = reservations
                             .Join(
           resources,
           reservation => reservation.ResourceId,
           resource => resource.ResourceId,
           (reservation, resource) => resource.Name)
                         .GroupBy(resourceName => resourceName)
                         .ToDictionary(g => g.Key, g => g.Count());

            return new GetResourceUsageReportResponse
            {
                ResourceUsages = usageByName
            };
        }

        private ResourceInfo MapToResourceInfo(Models.Resource resource)
        {
            return new ResourceInfo
            {
                ResourceId = resource.ResourceId,
                Name = resource.Name,
                Description = resource?.Description,
                Type = resource.Type,
                IsAvailable = resource.IsAvailable,
            };
        }
    }
}