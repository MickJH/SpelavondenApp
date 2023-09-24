using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Entities
{
    public class BoardGame
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Beschrijving is verplicht.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Genre is verplicht.")]
        public GenreType? Genre { get; set; } = new GenreType();

        [Display(Name = "Is 18+")]
        public bool Is18Plus { get; set; }

        [Display(Name = "Foto URL")]
        public string? PhotoUrl { get; set; }

        [Display(Name = "Speltype")]
        public GameType? GameType { get; set; } = new GameType();
    }

    [Owned]
    public class GameType
    {
        [Required(ErrorMessage = "Speltype is verplicht.")]
        public string? Type { get; set; }
    }

}
