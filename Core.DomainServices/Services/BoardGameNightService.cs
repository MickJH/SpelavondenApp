using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.DomainServices.Services.Interfaces;
using Core.DomainServices.Repositories.Interfaces;
using Core.Domain.Entities;

namespace Core.DomainServices.Services
{
    public class BoardGameNightService : IBoardGameNightService
    {
        private readonly IBoardGameNightRepository _boardGameNightRepository;

        public BoardGameNightService(IBoardGameNightRepository boardGameNightRepository)
        {
            _boardGameNightRepository = boardGameNightRepository;
        }

        public async Task<IEnumerable<BoardGameNight>> GetAllBoardGameNightsAsync()
        {
            return await _boardGameNightRepository.GetAllBoardGameNightsAsync();
        }

        public async Task<BoardGameNight> GetBoardGameNightByIdAsync(int id)
        {
            return await _boardGameNightRepository.GetBoardGameNightByIdAsync(id);
        }

        public async Task<BoardGameNight> CreateBoardGameNightAsync(BoardGameNight boardGameNight)
        {
            return await _boardGameNightRepository.CreateBoardGameNightAsync(boardGameNight);
        }

        public async Task UpdateBoardGameNightAsync(int id, BoardGameNight boardGameNight)
        {
            var existingBoardGameNight = await _boardGameNightRepository.GetBoardGameNightByIdAsync(id);
            if (existingBoardGameNight == null)
            {
                throw new ArgumentException("BoardGameNight not found");
            }

            // Update properties of the existing boardGameNight with new values.
            existingBoardGameNight.Address = boardGameNight.Address;
            existingBoardGameNight.DateAndTime = boardGameNight.DateAndTime;
            existingBoardGameNight.MaxPlayers = boardGameNight.MaxPlayers;
            existingBoardGameNight.Players = boardGameNight.Players;
            existingBoardGameNight.Games = boardGameNight.Games;
            existingBoardGameNight.FoodAndDrinkOptions = boardGameNight.FoodAndDrinkOptions;

            await _boardGameNightRepository.UpdateBoardGameNightAsync(existingBoardGameNight);
        }

        public async Task DeleteBoardGameNightAsync(int id)
        {
            await _boardGameNightRepository.DeleteBoardGameNightAsync(id);
        }
    }
}
