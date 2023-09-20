using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class BoardGameNight
    {
        public int Id { get; set; }
        public string? OrganizerId { get; set; }
        public string? OrganizerName { get; set; }
        public string? Address { get; set; }
        public DateTime DateAndTime { get; set; }
        public int MaxPlayers { get; set; }

        public ICollection<Person>? Players { get; set; }
        public ICollection<BoardGame>? Games { get; set; }
        public FoodAndDrinkOption? FoodAndDrinkOptions { get; set; }
        public int SelectedBoardGameId { get; set; }
    }
}

