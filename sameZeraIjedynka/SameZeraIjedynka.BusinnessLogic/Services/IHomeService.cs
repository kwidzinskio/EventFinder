using SameZeraIjedynka.Database.Entities;

namespace SameZeraIjedynka.BusinnessLogic.Services
{
    public interface IHomeService
    {
        Task<List<Event>> GetHomeEvents();
    }
}