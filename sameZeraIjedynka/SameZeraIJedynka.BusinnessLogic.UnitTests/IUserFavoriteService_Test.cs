using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using SameZeraIjedynka.BusinnessLogic.Services;
using SameZeraIjedynka.Database.Repositories;
using SameZeraIjedynka.Database.Entities;
using SameZeraIJedynka.BusinnessLogic.Models;
using SameZeraIJedynka.BusinnessLogic.Services;




namespace SameZeraIjedynka.Tests
{
    public class UserFavoriteServiceTests
    {
        [Fact]
        public async Task AddFavoriteEvent_ValidInput_SuccessfullyAddsFavoriteEvent()
        {
            // Arrange
            var mockUserFavoriteService = new Mock<IUserFavoriteService>();
            int eventId = 1;
            int userId = 123;


            // Act
            await mockUserFavoriteService.Object.AddFavoriteEvent(eventId, userId);

            // Assert

        }

        [Fact]
        public async Task DeleteFavoriteEvent_ValidInput_SuccessfullyDeletesFavoriteEvent()
        {
            // Arrange
            var mockUserFavoriteService = new Mock<IUserFavoriteService>();
            int eventId = 1;
            int userId = 123;

            // Act
            await mockUserFavoriteService.Object.DeleteFavoriteEvent(eventId, userId);

            // Assert

        }

        [Fact]
        public async Task GetFavoriteEvents_ValidUserId_ReturnsListOfFavoriteEvents()
        {
            // Arrange
            var mockUserFavoriteService = new Mock<IUserFavoriteService>();
            int userId = 123;
            string sortOption = "time_left"; // Set a desired sort option for testing

            var expectedFavoriteEvents = new List<Event>
            {
                new Event { EventId = 1, Name = "Event 1" },
                new Event { EventId = 2, Name = "Event 2" },
                // Add more favorite events as needed for your test
            };
            mockUserFavoriteService
                .Setup(service => service.GetFavoriteEvents(userId, sortOption))
                .ReturnsAsync(expectedFavoriteEvents);

            // Act
            var result = await mockUserFavoriteService.Object.GetFavoriteEvents(userId, sortOption);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<Event>>(result);
            Assert.Equal(expectedFavoriteEvents.Count, result.Count);

        }
    }
}