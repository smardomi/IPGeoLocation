﻿using System.Text.Json.Serialization;

namespace IPGeoLocation.Models
{
    public class GeolocationResult
    {
        public string? Query { get; set; }
        public string? Status { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Region { get; set; }
        public string? RegionName { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        public string? Timezone { get; set; }
        public string? Isp { get; set; }
        public string? Org { get; set; }
        public string? As { get; set; }

        [JsonIgnore]
        public bool FromIndex { get; set; } = false;
    }
}
