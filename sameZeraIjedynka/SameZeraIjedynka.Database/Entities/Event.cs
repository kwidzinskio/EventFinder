using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SameZeraIjedynka.Database.Entities.TargetEnum;

namespace SameZeraIjedynka.Database.Entities
{
    public class Event
    {    
        public int EventId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Organizer { get; set; }

        public string Place { get; set; }

        public int Price { get; set; }

        public int Capacity { get; set; }

        public TargetEnum Target { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<UserFavorite> EventFavorites { get; set; }

        public string? Description { get; set; } 

        public string? ImagePath { get; set; }
    }
}
