using Microsoft.AspNetCore.Mvc;

namespace Portal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        [Display(Name = "E-mailadres")]
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "Dit e-mailadres is al in gebruik.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Geslacht is verplicht.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Adres is verplicht.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [AgeValidation(ErrorMessage = "Je moet ouder zijn dan 16 jaar.")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [StringLength(100, ErrorMessage = "{0} moet minimaal {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Wachtwoord is verplicht.")]
        [RegularExpression(@"^(?=.*\W).*$", ErrorMessage = "Wachtwoord moet minstens 1 speciaal karakter bevatten.")]
        [Display(Name = "Wachtwoord")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Wachtwoord bevestigen is verplicht.")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord bevestigen")]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        public string? ConfirmPassword { get; set; }
    }


    public class AgeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                // Assume the value is valid since a required field validator should be used
                return true;
            }

            var birthDate = (DateTime)value;
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            return age >= 16;
        }
    }

}
