using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana do rejestracji")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany do rejestracji")]
        [EmailAddress(ErrorMessage = "Należy wpisać poprawny adres email np. jan.kowalski@poczta.pl")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane do rejestracji")]
        [StringLength(100, MinimumLength =8, ErrorMessage = "Hasło musi mieć przynajmniej 8 znaków długości")]
        public string Password { get; set; }
    }
}
