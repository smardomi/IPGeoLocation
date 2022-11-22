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

        public async Task<GeolocationResult?> GetIpGeolocationAsync(string ip)
        {
            var response = await _client.GetAsync($"/json/{ip}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<GeolocationResult>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
