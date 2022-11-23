using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using IPGeoLocation.Models;

namespace IPGeoLocation.Services
{
    public class GeolocationService : IGeolocationService
    {
        #region ctor

        private readonly HttpClient _client;

        public GeolocationService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        #endregion

        public async Task<GeolocationResult?> GetIpGeolocationAsync(string? ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
               
            }

            var response = await _client.GetAsync($"/json/{ip}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GeolocationResult>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
