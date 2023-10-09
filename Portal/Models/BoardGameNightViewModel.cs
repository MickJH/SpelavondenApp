using Core.Domain.Entities;

namespace Portal.Models
{
    public class BoardGameNightViewModel
    {
        public IEnumerable<BoardGameNight>? AllBoardGameNights { get; set; }
        public IEnumerable<BoardGameNight>? OrganizedBoardGameNights { get; set; }
        public IEnumerable<BoardGameNight>? JoinedBoardGameNights { get; set; }
    }

}