using OfficeResourceBookingSystem.Services.DTOs.Authentication;

namespace OfficeResourceBookingSystem.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
