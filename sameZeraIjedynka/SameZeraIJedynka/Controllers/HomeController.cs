using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SameZeraIjedynka.BusinnessLogic.Services;
using SameZeraIjedynka.Database.Context;
using SameZeraIJedynka.BusinnessLogic.Models;
using SameZeraIJedynka.BusinnessLogic.Services;
using System.Diagnostics;

namespace SameZeraIJedynka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService, ILogger<HomeController> logger)  
        {
            this.homeService = homeService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var events = await homeService.GetHomeEvents();
            return View(events);
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
    }
}