using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.Domain.Entities;


namespace Core.DomainServices.Services.Interfaces
{
    public interface IBoardGameNightService
    {
        Task<IEnumerable<BoardGameNight>> GetAllBoardGameNightsAsync();
        Task<BoardGameNight> GetBoardGameNightByIdAsync(int id);
        Task<BoardGameNight> CreateBoardGameNightAsync(BoardGameNight boardGameNight);
        Task UpdateBoardGameNightAsync(int id, BoardGameNight boardGameNight);
        Task DeleteBoardGameNightAsync(int id);
    }
}
