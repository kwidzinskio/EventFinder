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
using Microsoft.AspNetCore.Http.Internal;

namespace SameZeraIJedynka.Tests
{
    public class EventServiceTests
    {
        private Mock<IEventRepository> mockEventRepository;
        private EventService eventService;

        public EventServiceTests()
        {
            // Create a mock event repository and pass it to the EventService
            mockEventRepository = new Mock<IEventRepository>();
            eventService = new EventService(mockEventRepository.Object);
        }

        // Helper method to create a sample IFormFile for testing
        private static IFormFile CreateMockFormFile(string fileName)
        {
            var stream = new MemoryStream(new byte[0]);
            return new FormFile(stream, 0, 0, "Data", fileName);
        }

        [Fact]
        public async Task Add_ValidData_ReturnsNewEventId()
        {
            // Arrange
            var addEventRequest = new EventModel
            {
                // Set properties for the event model you want to test
                Name = "Sample Event",
                Date = DateTime.Now,
                // Add other properties
            };
            var image = CreateMockFormFile("test.jpg");
            var userId = 123;

            // Setup the eventRepository mock to return the new event ID when AddEvent is called
            mockEventRepository
                .Setup(repo => repo.AddEvent(It.IsAny<Event>()))
                .ReturnsAsync(1); // Set a desired new event ID

            // Act
            var newEventId = await eventService.Add(addEventRequest, image, userId);

            // Assert
            Assert.Equal(1, newEventId); // Check if the expected new event ID is returned
           
        }

        [Fact]
        public async Task Index_ValidSortOption_ReturnsSortedEvents()
        {
            // Arrange
            var sortOption = "time_left";
            var expectedEvents = new List<Event>
            {
                new Event { Name = "Event 1", Date = DateTime.Now.AddDays(1) },
                new Event { Name = "Event 2", Date = DateTime.Now.AddDays(2) },
                // Add more events with different dates for testing sorting
            };
            var mockEventsQuery = expectedEvents.AsQueryable();

            // Setup the eventRepository mock to return the mockEventsQuery when Get is called
            mockEventRepository
                .Setup(repo => repo.Get())
                .ReturnsAsync(mockEventsQuery);

            // Act
            var result = await eventService.Index(sortOption);

            // Assert
            Assert.Equal(expectedEvents.OrderBy(e => e.Date), result);
        }
    }
}