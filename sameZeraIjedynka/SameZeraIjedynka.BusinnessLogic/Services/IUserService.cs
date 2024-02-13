using SameZeraIjedynka.BusinnessLogic.Models;
using SameZeraIjedynka.Database.Entities;
using SameZeraIJedynka.Models;

namespace SameZeraIjedynka.BusinnessLogic.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserModelById(int id);
        Task UpdateUser(UserModel user, UserModel model);
        Task AddUser(RegisterUserModel addUserRequest);
        Task<bool> IsUsernameUnique(string username);
        Task<int> GetUserId(LoginUserModel model);
        Task<bool> AuthenticateUser(LoginUserModel user);
        Task SendEmail(RegisterUserModel user);
    }
}