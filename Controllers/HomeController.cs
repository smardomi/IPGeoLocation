using IPGeoLocation.Models;
using IPGeoLocation.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IPGeoLocation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IGeolocationService geolocationService;


        public HomeController(ILogger<HomeController> logger, IGeolocationService geolocationService)
        {
            this.logger = logger;
            this.geolocationService = geolocationService;
        }

        public async Task<IActionResult> Index()
        {
            var geoLocation = await geolocationService.GetIpGeolocationAsync(null);
            geoLocation.FromIndex = true;
            return View(geoLocation);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task<IActionResult> GetIpGeoLocation(string ip)
        {
            var geoLocation = await geolocationService.GetIpGeolocationAsync(ip);
            return PartialView("_GeoLocation", geoLocation);
        }
    }
}