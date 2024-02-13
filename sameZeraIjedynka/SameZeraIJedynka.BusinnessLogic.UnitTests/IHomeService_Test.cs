using Moq;
using SameZeraIjedynka.BusinnessLogic.Services;
using SameZeraIjedynka.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SameZeraIjedynka.Tests
{
    public class IHomeServiceTests
    {
        [Fact]
        public async Task GetHomeEvents_ReturnsListOfEvents()
        {
            // Arrange
            var mockHomeService = new Mock<IHomeService>();
            var expectedEvents = new List<Event>
            {
                new Event { EventId = 1,
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
                      ImagePath = "/assets/img/1.jpg" }
                // Add more events as needed for your test
            };

            // Setup the mockHomeService to return the expectedEvents when GetHomeEvents is called
            mockHomeService
                .Setup(service => service.GetHomeEvents())
                .ReturnsAsync(expectedEvents);

            // Act
            var result = await mockHomeService.Object.GetHomeEvents();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<Event>>(result);
            Assert.Equal(expectedEvents.Count, result.Count);
          
        }
    }
}
