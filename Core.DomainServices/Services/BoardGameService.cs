using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.DomainServices.Services.Interfaces;
using Core.DomainServices.Repositories.Interfaces;
using Core.Domain.Entities;

namespace Core.DomainServices.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IBoardGameRepository _boardGameRepository;

        public BoardGameService(IBoardGameRepository boardGameRepository)
        {
            _boardGameRepository = boardGameRepository;
        }

        public async Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync()
        {
            return await _boardGameRepository.GetAllBoardGamesAsync();
        }

        public async Task<BoardGame> GetBoardGameByIdAsync(int id)
        {
            return await _boardGameRepository.GetBoardGameByIdAsync(id);
        }

        public async Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame)
        {
            return await _boardGameRepository.CreateBoardGameAsync(boardGame);
        }

        public async Task UpdateBoardGameAsync(int id, BoardGame boardGame)
        {
            await _boardGameRepository.UpdateBoardGameAsync(boardGame);
        }

        public async Task DeleteBoardGameAsync(int id)
        {
            await _boardGameRepository.DeleteBoardGameAsync(id);
        }
    }

}

