using OfficeResourceBookingSystem.Repository.Base;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Resource
{
    public interface IResourceRepository : IBaseRepository<Models.Resource, ResourceFilter, ResourceUpdate>
    {
    }
}
