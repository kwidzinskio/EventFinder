using SameZeraIjedynka.Database.Entities;

namespace SameZeraIjedynka.Database.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User newUser);
        Task<bool> Authenticate(string username, string password);
        Task<int> FindUserId(string username, string password);
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string username);
        Task UpdateUser(User user, string newUsername, string newEmail, string newPassword);
    }
}