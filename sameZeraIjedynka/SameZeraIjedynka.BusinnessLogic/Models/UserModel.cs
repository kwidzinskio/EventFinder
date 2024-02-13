using System.ComponentModel.DataAnnotations;

namespace SameZeraIJedynka.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Nazwa użytkownika musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Email musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Email użytkownika jest wymagany")]
        public string Email { get; set; }
        [MinLength(2, ErrorMessage = "Hasło musi mieć przynajmniej dwa znaki.")]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; }
    }
}
