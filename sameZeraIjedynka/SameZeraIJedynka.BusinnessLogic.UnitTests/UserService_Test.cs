
using Xunit;
using Moq;
using SameZeraIjedynka.BusinnessLogic.Models;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Repositories;
using SameZeraIjedynka.BusinnessLogic.Services;
using MailKit;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace SameZeraIjedynka.Tests
{
    public class UserService_Tests
    {
        [Fact]
        public async Task GetUserModelById_ValidId_ReturnsUserModel()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);
            int userId = 1;
            var mockUser = new User
            {
                UserId = userId,
                Name = "TestUser",
                Password = "HashedPassword"
            };
            mockUserRepository
                .Setup(repo => repo.GetUserById(userId))
                .ReturnsAsync(mockUser);

            // Act
            var result = await userService.GetUserModelById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(mockUser.Name, result.Name);
            Assert.Equal(mockUser.Password, result.Password);
        }

    }
}
