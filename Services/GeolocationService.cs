using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using IPGeoLocation.Models;

namespace IPGeoLocation.Services
{
    public class GeolocationService : IGeolocationService
    {
        #region ctor

        private static readonly string GetExternalIpApi = "https://api.ipify.org";
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
                ip = await GetExternalIpAddress();
            }

            var response = await _client.GetAsync($"/json/{ip}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GeolocationResult>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private string GetLocalIpAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            string ip = addr[2].ToString();
            return ip;
        }

        public async Task<string> GetExternalIpAddress()
        {
            try
            {
                var externalIp = await _client.GetStringAsync(GetExternalIpApi);
                return externalIp;
            }
            catch { return GetLocalIpAddress(); }
        }
    }
}
