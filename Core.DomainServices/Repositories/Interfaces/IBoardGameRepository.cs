using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.DomainServices.Repositories.Interfaces
{
    public interface IBoardGameRepository
    {
        Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync();
        Task<BoardGame> GetBoardGameByIdAsync(int id);
        Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame);
        Task UpdateBoardGameAsync(BoardGame boardGame);
        Task DeleteBoardGameAsync(int id);
    }
}



