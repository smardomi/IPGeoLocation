using System.Net;

namespace IPGeoLocation.Extensions
{
    public static class HttpExtension
    {
        private static readonly string GetExternalIpApi = "https://api.ipify.org";

        public static string GetLocalIpAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            string ip = addr[2].ToString();
            return ip;
        }

        public static async Task<string> GetExternalIpAddress()
        {
            try
            {
                using var client = new HttpClient();
                var externalIp = await client.GetStringAsync(GetExternalIpApi);
                return externalIp ?? GetLocalIpAddress();
            }
            catch { return GetLocalIpAddress(); }
        }

        public static string GetClientIpAddress(IHttpContextAccessor httpContextAccessor)
        {
            var ipAddress = httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            }
            else if (ipAddress.Contains(","))
            {
                ipAddress = ipAddress.Split(",")[0].Trim();
            }
            return ipAddress ?? GetLocalIpAddress();
        }
    }
}
