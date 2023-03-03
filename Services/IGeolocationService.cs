using IPGeoLocation.Models;

namespace IPGeoLocation.Services
{
    public interface IGeolocationService
    {
        Task<GeolocationResult> GetIpGeolocationAsync(string? ip);
    }
}
