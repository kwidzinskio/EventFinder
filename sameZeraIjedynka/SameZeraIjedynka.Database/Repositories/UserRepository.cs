using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Context;
using SameZeraIjedynka.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SameZeraIjedynka.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly DatabaseContext context;
        public UserRepository(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            return user;
        }

        public async Task UpdateUser(User user, string newUsername, string newEmail, string newPassword)
        {
            user.Name = newUsername;
            user.Password = newPassword;
            user.Email = newEmail;
            await context.SaveChangesAsync();
        }

        public async Task<bool> Authenticate(string username, string hashedPassword)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == username && u.Password == hashedPassword);

            return user != null;
        }

        public async Task<int> FindUserId(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
            var userId = user.UserId;

            return userId;
        }

        public async Task AddUser(User newUser)
        {
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByName(string username)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Name == username);
        }
    }
}
