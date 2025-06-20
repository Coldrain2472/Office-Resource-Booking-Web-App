namespace OfficeResourceBookingSystem.Web.Models.Resource
{
    public class ResourceListViewModel
    {
        public List<ResourceViewModel> Resource { get; set; }

        public int TotalCount { get; set; }
    }

    public class ResourceViewModel
    {
        public int ResourceId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}
