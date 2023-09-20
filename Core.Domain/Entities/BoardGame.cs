using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Entities
{
    public class BoardGame
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public GenreType? Genre { get; set; } // Enum representing genres.
        public bool Is18Plus { get; set; }
        public string? PhotoUrl { get; set; }
        public GameType? GameType { get; set; } // Enum representing game types (e.g., CardGame)
    }

    [Owned]
    public class GameType
    {
        public string? Type { get; set; } // Define properties for your GameType enum, e.g., Type
    }

    [Owned]
    public class GenreType
    {
        public string? Genre { get; set; } // Define properties for your GenreType enum, e.g., Genre
    }
}
