using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Context;
using SameZeraIJedynka.BusinnessLogic.Models;
using Newtonsoft.Json;
using NuGet.ContentModel;
using SameZeraIJedynka.BusinnessLogic.Services;

namespace SameZeraIJedynka.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService) 
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventModel addEventRequest, IFormFile image)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var newEventId = await eventService.Add(addEventRequest, image, userId.Value);

                return RedirectToAction("EventDetails", new { id = newEventId });
            }

            return RedirectToAction("Login", "Users");

        }

		[HttpGet]
		public async Task<IActionResult> Index(string sortOption = null)
		{
			var eventsQuery = await eventService.Index(sortOption);
			var events = await eventsQuery.ToListAsync();

			return View(events);
		}

        [HttpGet]
        public async Task<IActionResult> Search(string searchPattern, string sortOption = null)
        {
			ViewData["SearchPattern"] = searchPattern;
			var eventsQuery = await eventService.Search(searchPattern, sortOption);

            return View(eventsQuery);
        }

		[HttpGet]
        public async Task<IActionResult> EventDetails(int id)
		{
            var eventQuery = await eventService.EventDetails(id);

            return View(eventQuery);
		}

	}
    
}
