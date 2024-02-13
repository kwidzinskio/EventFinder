using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SameZeraIjedynka.Database.Context;
using SameZeraIjedynka.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameZeraIjedynka.Database.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext context;
        public EventRepository(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<int> AddEvent(Event newEvent)
        {
            await context.Events.AddAsync(newEvent);
            await context.SaveChangesAsync();

            return newEvent.EventId;
        }

        public async Task<IQueryable<Event>> Get()
        {
            IQueryable<Event> eventsQuery = context.Events;

            return eventsQuery;
        }

        public async Task<IQueryable<Event>> Search(string searchPattern)
        {
            IQueryable<Event> eventsQuery = context.Events.Where(e => e.Name.Contains(searchPattern) || 
                                                                      e.Description.Contains(searchPattern) || 
                                                                      e.Organizer.Contains(searchPattern));

            return eventsQuery;
        }

        public async Task<Event> GetById(int eventId)
        {
            Event eventsQuery = await context.Events.FirstOrDefaultAsync(x => x.EventId == eventId);

            return eventsQuery;
        }

        public async Task Delete(Event eventsQuery)
        {
            context.Events.Remove(eventsQuery);
            await context.SaveChangesAsync();
        }

        public async Task<IQueryable<Event>> GetHomeEvents()
        {
            IQueryable<Event> eventsQuery = context.Events.Take(3);

            return eventsQuery;
        }
        public async Task<List<Event>> GetUsersEvents(int userId)
        {
            return context.Events.Where(u => u.UserId == userId).ToList();
        }

        public async Task<bool> EventBelongsToUser(int userId, int eventId)
        {
            var events = context.Events.FirstOrDefault(e => e.UserId == userId && e.EventId == eventId);

            return events != null;
        }

        public async Task UpdateEvent(int eventId, Event newEvent)
        {
            var existingEvent = await context.Events.FindAsync(eventId);

            existingEvent.Name = newEvent.Name;
            existingEvent.Date = newEvent.Date;
            existingEvent.Organizer = newEvent.Organizer;
            existingEvent.Place = newEvent.Place;
            existingEvent.Price = newEvent.Price;
            existingEvent.Capacity = newEvent.Capacity;
            existingEvent.Target = newEvent.Target;
            existingEvent.Description = newEvent.Description;
            existingEvent.ImagePath = newEvent.ImagePath;

            context.Events.Update(existingEvent);
            await context.SaveChangesAsync();
        }
    }
}
