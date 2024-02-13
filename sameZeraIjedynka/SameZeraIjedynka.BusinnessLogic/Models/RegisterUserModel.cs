using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameZeraIjedynka.BusinnessLogic.Models
{
    public class RegisterUserModel
    {
        [MinLength(2, ErrorMessage = "Nazwa użytkownika musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Email musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Email użytkownika jest wymagany.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [MinLength(2, ErrorMessage = "Hasło musi mieć przynajmniej dwa znaki.")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }
}
