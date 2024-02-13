using SameZeraIjedynka.Database.Entities;

namespace SameZeraIjedynka.Database.Repositories
{
    public interface IEventRepository
    {
        Task<int> AddEvent(Event newEvent);
        Task Delete(Event eventsQuery);
        Task<IQueryable<Event>> Get();
        Task<Event> GetById(int eventId);
        Task<IQueryable<Event>> GetHomeEvents();
        Task<IQueryable<Event>> Search(string searchPattern);
        Task<List<Event>> GetUsersEvents(int userId);
        Task<bool> EventBelongsToUser(int userId, int eventId);
        Task UpdateEvent(int eventId, Event newEvent);
    }
}