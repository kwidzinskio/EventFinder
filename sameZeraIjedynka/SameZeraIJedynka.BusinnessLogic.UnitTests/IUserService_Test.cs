
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using SameZeraIjedynka.BusinnessLogic.Services;
using SameZeraIjedynka.Database.Repositories;
using SameZeraIjedynka.Database.Entities;
using SameZeraIJedynka.Models;

namespace SameZeraIjedynka.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUserModelById_ValidId_ReturnsUserModel()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            int userId = 1;
            var expectedUserModel = new UserModel
            {
                // Set properties for the expected user model you want to test
                // Example: Username, Email, etc.
            };

            // Setup the mockUserService to return the expectedUserModel when GetUserModelById is called
            mockUserService
                .Setup(service => service.GetUserModelById(userId))
                .ReturnsAsync(expectedUserModel);

            // Act
            var result = await mockUserService.Object.GetUserModelById(userId);

            // Assert
            Assert.NotNull(result);
            
        }

        [Fact]
        public async Task UpdateUser_ValidInput_SuccessfullyUpdatesUser()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var user = new UserModel
            {
                // Set properties for the user you want to update
                // Example: Username, Email, etc.
            };
            var model = new UserModel
            {
                // Set properties for the updated user model
                // Example: Updated Username, Email, etc.
            };


            // Act
            await mockUserService.Object.UpdateUser(user, model);

            // Assert
 
        }

  
    }
}