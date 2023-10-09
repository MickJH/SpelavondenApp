using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.DomainServices.Repositories.Interfaces
{
    public interface IBoardGameNightRepository
    {
        Task<IEnumerable<BoardGameNight>> GetAllBoardGameNightsAsync();
        Task<BoardGameNight> GetBoardGameNightByIdAsync(int id);
        Task<BoardGameNight> CreateBoardGameNightAsync(BoardGameNight boardGameNight);
        Task UpdateBoardGameNightAsync(BoardGameNight boardGameNight);
        Task DeleteBoardGameNightAsync(int id);
        Task<IEnumerable<BoardGameNight>> GetBoardGameNightsByOrganizerAsync(string organizerName);
        Task<IEnumerable<BoardGameNight>> GetBoardGameNightsByParticipantAsync(string participantName);
    }
}
