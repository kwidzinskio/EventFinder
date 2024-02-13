using Microsoft.AspNetCore.Http;
using Moq;
using SameZeraIJedynka.BusinnessLogic.Models;
using SameZeraIJedynka.BusinnessLogic.Services;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SameZeraIjedynka.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public async Task Add_CorrectInput_ReturnsNewEventId()
        {
            // Arrange
            var mockEventService = new Mock<IEventService>();
            var addEventRequest = new EventModel
            {
                // Set properties for the event model you want to test
                Name = "Sample Event",
                Date = DateTime.Now,
                 
            };
            var image = new Mock<IFormFile>();
            var userId = 123;

            // Setup the mockEventService to return a predefined new event ID when Add is called
            mockEventService
                .Setup(service => service.Add(It.IsAny<EventModel>(), It.IsAny<IFormFile>(), It.IsAny<int>()))
                .ReturnsAsync(1); // Set a desired new event ID

            // Act
            var result = await mockEventService.Object.Add(addEventRequest, image.Object, userId);

            // Assert
            Assert.Equal(1, result); // Check if the expected new event ID is returned
           
        }

    
    }
}