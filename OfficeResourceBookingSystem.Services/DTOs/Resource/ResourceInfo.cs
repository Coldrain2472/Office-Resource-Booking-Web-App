namespace OfficeResourceBookingSystem.Services.DTOs.Resource
{
    public class ResourceInfo
    {
        public int ResourceId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Type { get; set; }

        public bool IsAvailable { get; set; }
    }
}
