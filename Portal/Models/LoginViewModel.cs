using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        [Display(Name = "E-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [DataType(DataType.Password, ErrorMessage = "Wachtwoord is verplicht.")]
        [Display(Name = "Wachtwoord")]
        public string? Password { get; set; }
    }
}
