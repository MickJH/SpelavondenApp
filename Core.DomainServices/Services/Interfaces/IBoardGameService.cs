using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.DomainServices.Services.Interfaces
{
    public interface IBoardGameService
    {
        Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync();
        Task<BoardGame> GetBoardGameByIdAsync(int id);
        Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame);
        Task UpdateBoardGameAsync(int id, BoardGame boardGame);
        Task DeleteBoardGameAsync(int id);
    }
}
