﻿using IPGeoLocation.Models;
using IPGeoLocation.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IPGeoLocation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGeolocationService _geolocationService;


        public HomeController(ILogger<HomeController> logger, IGeolocationService geolocationService)
        {
            _logger = logger;
            _geolocationService = geolocationService;
        }

        public async Task<IActionResult> Index()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var geoLocation = await _geolocationService.GetIpGeolocationAsync(remoteIpAddress);
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
            var geoLocation = await _geolocationService.GetIpGeolocationAsync(ip);
            return PartialView("_GeoLocation", geoLocation);
        }
    }
}