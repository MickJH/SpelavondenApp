using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class BoardGameNight
    {
        public int Id { get; set; }
        public string? OrganizerId { get; set; }
        public string? OrganizerName { get; set; }

        [Required(ErrorMessage = "Adres is verplicht.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Datum en tijd is verplicht.")]
        [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [FutureDate(ErrorMessage = "Spelavond moet minimaal 24 uur van te voren worden aangemaakt.")]
        public DateTime DateAndTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Max aantal spelers is verplicht.")]
        public int MaxPlayers { get; set; }

        [Display(Name = "Is 18+")]
        public bool Is18Plus { get; set; }

        [Display(Name = "18+ (Override)")]

        public bool IsOrganizerOverride18Plus { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public ICollection<BoardGame>? Games { get; set; }
        public FoodAndDrinkOption? FoodAndDrinkOptions { get; set; }

        public BoardGame? SelectedBoardGame { get; set; }

        [Required(ErrorMessage = "Spel is verplicht.")]
        public int? SelectedBoardGameId { get; set; }
    }


    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime > DateTime.Now.AddHours(24);
            }
            return false;  // Invalid type
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime JoinDateTime { get; set; }
    }
}
