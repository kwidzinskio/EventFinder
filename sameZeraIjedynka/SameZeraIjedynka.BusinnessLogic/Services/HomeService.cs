using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameZeraIjedynka.BusinnessLogic.Services
{
    public class HomeService : IHomeService
    {
        private readonly IEventRepository eventRepository;

        public HomeService(IEventRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }

        public async Task<List<Event>> GetHomeEvents()
        {
            IQueryable<Event> eventsQuery = await eventRepository.GetHomeEvents();
            var events = eventsQuery.ToList();

            return events;
        }
    }
}
