using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities
{
    public class Person : IdentityUser
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Gender is verplicht.")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Adres is verplicht.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        public DateTime? Birthdate { get; set; }
        public bool HasLactoseAllergy { get; set; }
        public bool HasNutAllergy { get; set; }
        public bool IsVegetarian { get; set; }
        public bool AvoidsAlcohol { get; set; }
    }
}


