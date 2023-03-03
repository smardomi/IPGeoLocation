using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using IPGeoLocation.Extensions;
using IPGeoLocation.Models;

namespace IPGeoLocation.Services
{
    public class GeolocationService : IGeolocationService
    {
        #region ctor
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient client;

        public GeolocationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            client = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        public async Task<GeolocationResult> GetIpGeolocationAsync(string? ip)
        {
            ip = GetIpAddress(ip);

            var response = await client.GetAsync($"/json/{ip}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GeolocationResult>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new GeolocationResult();
        }

        private string GetIpAddress(string? ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = HttpExtension.GetClientIpAddress(httpContextAccessor);
            }

            return ip;
        }
    }
}
