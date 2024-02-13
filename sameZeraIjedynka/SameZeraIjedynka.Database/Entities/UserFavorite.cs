using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameZeraIjedynka.Database.Entities
{
    public class UserFavorite
    {
       // [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
       // [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
