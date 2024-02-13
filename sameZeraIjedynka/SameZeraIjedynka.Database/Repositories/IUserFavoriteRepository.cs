using SameZeraIjedynka.Database.Entities;

namespace SameZeraIjedynka.Database.Repositories
{
    public interface IUserFavoriteRepository
    {
        Task Add(UserFavorite newFavorite);
        Task Delete(UserFavorite newFavorite);
        Task<UserFavorite> Find(int id, int userId);
        Task<IQueryable<Event>> Get(int userId);
    }
}