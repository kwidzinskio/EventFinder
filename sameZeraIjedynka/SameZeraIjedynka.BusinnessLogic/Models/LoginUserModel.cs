using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameZeraIjedynka.BusinnessLogic.Models
{
    public class LoginUserModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Nazwa użytkownika musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Hasło musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; }
    }
}
