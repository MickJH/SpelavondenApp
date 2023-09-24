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
        public DateTime DateAndTime { get; set; }

        [Required(ErrorMessage = "Max aantal spelers is verplicht.")]
        public int MaxPlayers { get; set; }

        public ICollection<Person>? Players { get; set; }
        public ICollection<BoardGame>? Games { get; set; }
        public FoodAndDrinkOption? FoodAndDrinkOptions { get; set; }

        [Required(ErrorMessage = "Spel kiezen is verplicht.")]
        public int? SelectedBoardGameId { get; set; }
    }
}

