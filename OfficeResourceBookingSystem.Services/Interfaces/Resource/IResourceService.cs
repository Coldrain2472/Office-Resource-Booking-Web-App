using OfficeResourceBookingSystem.Services.DTOs.Resource;

namespace OfficeResourceBookingSystem.Services.Interfaces.Resource
{
    public interface IResourceService
    {
        Task<GetResourceResponse> GetByIdAsync(int resourceId);

        Task<GetAllResourcesResponse> GetAllAsync();

        Task<GetMostUsedResourcesResponse> GetMostUsedResources(); // Статистика за най-използвани ресурси

        Task<GetResourceUsageReportResponse> GetResourceUsageReport(); // Генериране на справки за използваемост на ресурсите
    }
}
