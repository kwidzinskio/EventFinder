using SameZeraIjedynka.Database.Entities;
using SameZeraIJedynka.BusinnessLogic.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using static SameZeraIjedynka.Database.Entities.TargetEnum;
using TargetEnum = SameZeraIjedynka.Database.Entities.TargetEnum;

namespace SameZeraIJedynka.BusinnessLogic.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Organizer { get; set; }
        public string Place { get; set; }
        public int Price { get; set; }
        public int Capacity { get; set; }
        public TargetEnum Target { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
