namespace OfficeResourceBookingSystem.Services.DTOs.Resource
{
    public class GetAllResourcesResponse
    {
        public List<ResourceInfo>? Resources { get; set; }

        public int TotalCount { get; set; } 
    }
}
