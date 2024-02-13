using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Context;
using SameZeraIJedynka.Models;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SameZeraIJedynka.Controllers
{
    public class EventController : Controller
    {
        private readonly DatabaseContext mvcDbContext;

        public EventController(DatabaseContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventModel addEventRequest, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string path = Path.Combine(@"wwwroot\assets\img\", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                addEventRequest.ImagePath = "/assets/img/" + fileName;
            }
            else
            {
                string fileName = "placeholder.png";
                addEventRequest.ImagePath = "/assets/img/" + fileName;
            }


            var newEvent = new Event()
            {
                EventId = addEventRequest.EventId,
                Name = addEventRequest.Name,
                Date = addEventRequest.Date,
                Organizer = addEventRequest.Organizer,
                Place = addEventRequest.Place,
                Price = addEventRequest.Price,
                Capacity = addEventRequest.Capacity,
                Target = addEventRequest.Target,
                Description = addEventRequest.Description,
                ImagePath = addEventRequest.ImagePath
            };
            await mvcDbContext.Events.AddAsync(newEvent);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOption = null)
        {
            IQueryable<Event> eventsQuery = mvcDbContext.Events;

            switch (sortOption)
            {
                case "time_left":
                    eventsQuery = eventsQuery.OrderBy(e => e.Date);
                    break;
                case "time_left_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Date);
                    break;
                case "price":
                    eventsQuery = eventsQuery.OrderBy(e => e.Price);
                    break;
                case "price_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Price);
                    break;
                default:
                    eventsQuery = eventsQuery;
                    break;
            }

            var events = await eventsQuery.ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchPattern)
        {
            IQueryable<Event> eventsQuery = mvcDbContext.Events.Where(e => e.Name.Contains(searchPattern));

            var events = await eventsQuery.ToListAsync();

            return View("Index", events);
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var events = await mvcDbContext.Events.FirstOrDefaultAsync(x => x.EventId == id);
            if (events != null)
            {
                var viewModel = new EventModel()
                {
                    EventId = events.EventId,
                    Name = events.Name,
                    Date = events.Date,
                    Organizer = events.Organizer,
                    Place = events.Place,
                    Price = events.Price,
                    Capacity = events.Capacity,
                    Target = events.Target
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(EventModel model)
        {
            var events = await mvcDbContext.Events.FindAsync(model.EventId);
            if (events != null)
            {
                events.EventId = model.EventId;
                events.Name = model.Name;
                events.Date = model.Date;
                events.Organizer = model.Organizer;
                events.Place = model.Place;
                events.Price = model.Price;
                events.Capacity = model.Capacity;
                events.Target = model.Target;

                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("View");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EventModel model)
        {
            var events = await mvcDbContext.Events.FindAsync(model.EventId);
            if (events != null)
            {
                mvcDbContext.Events.Remove(events);
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EventDetails(int id)
        {
            var eventObj = await mvcDbContext.Events.FirstOrDefaultAsync(x => x.EventId == id);
            return View(eventObj);
        }

    }

}
