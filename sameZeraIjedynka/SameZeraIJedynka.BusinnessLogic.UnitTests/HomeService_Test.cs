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
using SameZeraIjedynka.BusinnessLogic.Services;

namespace SameZeraIjedynka.Tests
{
    public class HomeServiceTests
    {
        [Fact]
        public async Task GetHomeEvents_ReturnsListOfEvents()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var homeService = new HomeService(mockEventRepository.Object);

            // Prepare test data for the mock events
            var mockEventsData = new List<Event>
            {
                new Event {  EventId = 1,
                      Name = "Warsztaty artystyczne",
                      Date = new DateTime(2023, 12, 06, 12, 00, 00),
                      Organizer = "Fundacja Dla Dziecka",
                      Place = "Gdansk, Zielona 23",
                      Price = 0,
                      Capacity = 100,
                      Target = TargetEnum.all,
                      Description = "Warsztaty rysunku dla dzieci to interaktywne i kreatywne zajęcia, " +
                      "które mają na celu rozwijanie umiejętności plastycznych u najmłodszych. Podczas " +
                      "wydarzenia dzieci będą miały okazję eksperymentować z różnymi technikami rysunku i" +
                      " tworzyć własne unikalne dzieła sztuki, pod opieką doświadczonych instruktorów. Warsztaty " +
                      "stwarzają przyjazną i inspirującą atmosferę, sprzyjającą rozwijaniu wyobraźni i twórczych " +
                      "umiejętności dzieci.",
                      ImagePath = "/assets/img/1.jpg" },
               
                // Add more events as needed for your test
            };
            var mockEventsQuery = mockEventsData.AsQueryable();

            // Setup the mockEventRepository to return the mockEventsQuery when GetHomeEvents is called
            mockEventRepository
                .Setup(repo => repo.GetHomeEvents())
                .ReturnsAsync(mockEventsQuery);

            // Act
            var result = await homeService.GetHomeEvents();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<Event>>(result);
            Assert.Equal(mockEventsData.Count, result.Count);
      
        }
    }
}
