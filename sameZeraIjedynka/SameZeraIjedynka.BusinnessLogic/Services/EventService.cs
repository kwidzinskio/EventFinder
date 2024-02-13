using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Context;
using SameZeraIJedynka.BusinnessLogic.Models;
using Microsoft.AspNetCore.Http;
using SameZeraIjedynka.Database.Repositories;

namespace SameZeraIJedynka.BusinnessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;
        public EventService(IEventRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }

        public async Task<int> Add(EventModel addEventRequest, IFormFile image, int userId)
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
                ImagePath = addEventRequest.ImagePath,
                UserId = userId
            };

            var newEventId = await eventRepository.AddEvent(newEvent);

            return newEventId;
        }

        public async Task<IQueryable<Event>> Index(string sortOption)
        {
            IQueryable<Event> eventsQuery = await eventRepository.Get();

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

            return eventsQuery;
        }

        public async Task<List<Event>> Search(string searchPattern, string sortOption)
        {
            IQueryable<Event> eventsQuery = await eventRepository.Search(searchPattern);
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
					var events = eventsQuery.ToList();

            return events;
        }

        public async Task Delete(int id)
        {
            var eventsQuery = await eventRepository.GetById(id);
            if (eventsQuery != null)
            {
                await eventRepository.Delete(eventsQuery);
            }
        }

        public async Task<Event> EventDetails(int eventId)
        {
            Event eventsQuery = await eventRepository.GetById(eventId);

            return eventsQuery;
        }

        public async Task<List<Event>> GetEventsForUser(int id)
        {
            var events = await eventRepository.GetUsersEvents(id);

            return events;
        }

        public async Task<EventModel> ConvertEventToEventModel(Event events)
        {
            var eventModel = new EventModel()
            {
                EventId = events.EventId,
                Name = events.Name,
                Date = events.Date,
                Organizer = events.Organizer,
                Place = events.Place,
                Price = events.Price,
                Capacity = events.Capacity,
                Target = events.Target,
                Description = events.Description,
                ImagePath = events.ImagePath
            };

            return eventModel;
        }

        public async Task<bool> EventBelongsToUser(int userId, int eventId)
        {
            return await eventRepository.EventBelongsToUser(userId, eventId);
        }

        public async Task Update(EventModel addEventRequest, IFormFile image, int userId)
        {
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
                UserId = userId
            };

            if (image != null && image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string path = Path.Combine(@"wwwroot\assets\img\", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                addEventRequest.ImagePath = "/assets/img/" + fileName;
                newEvent.ImagePath = addEventRequest.ImagePath;
            }
            else
            {
                var oldEvent = await eventRepository.GetById(newEvent.EventId);
                var oldEventPath = oldEvent.ImagePath;
                newEvent.ImagePath = oldEventPath;
            }
            
            await eventRepository.UpdateEvent(addEventRequest.EventId, newEvent);
        }

        public Task Delete(EventModel eventToDelete)
        {
            throw new NotImplementedException();
        }

        public Task Search(string searchPattern)
        {
            throw new NotImplementedException();
        }
    }
}
