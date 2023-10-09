using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.DomainServices.Repositories.Interfaces;
using Infrastructure;
using Core.Domain.Entities;

namespace Core.DomainServices.Repositories
{
    public class BoardGameNightRepository : IBoardGameNightRepository
    {
        private readonly ApplicationDbContext _context;

        public BoardGameNightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BoardGameNight>> GetAllBoardGameNightsAsync()
        {
            var boardGameNights = await _context.BoardGameNights
                .Include(bgn => bgn.Games)
                .Include(bgn => bgn.Players)
                .Include(bgn => bgn.SelectedBoardGame)
                .Include(bgn => bgn.FoodAndDrinkOptions)
                .ToListAsync();

            return boardGameNights;
        }

        public async Task<BoardGameNight> GetBoardGameNightByIdAsync(int id)
        {
            var boardGameNight = await _context.BoardGameNights
                .Include(bgn => bgn.Games)
                .Include(bgn => bgn.Players)
                .Include(bgn => bgn.FoodAndDrinkOptions)
                .FirstOrDefaultAsync(bgn => bgn.Id == id)!;

            return boardGameNight!;
        }


        public async Task<BoardGameNight> CreateBoardGameNightAsync(BoardGameNight boardGameNight)
        {
            _context.BoardGameNights.Add(boardGameNight);
            await _context.SaveChangesAsync();
            return boardGameNight;
        }

        public async Task UpdateBoardGameNightAsync(BoardGameNight boardGameNight)
        {
            _context.Entry(boardGameNight).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoardGameNightAsync(int id)
        {
            var boardGameNight = await _context.BoardGameNights
                .FirstOrDefaultAsync(bgn => bgn.Id == id);

            _context.BoardGameNights.Remove(boardGameNight);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BoardGameNight>> GetBoardGameNightsByOrganizerAsync(string organizerName)
        {
            return await _context.BoardGameNights
                .Where(bgn => bgn.OrganizerName == organizerName)
                .ToListAsync();
        }

        public async Task<IEnumerable<BoardGameNight>> GetBoardGameNightsByParticipantAsync(string participantName)
        {
            return await _context.BoardGameNights
                .Where(bgn => bgn.Players.Any(player => player.Name == participantName))
                .ToListAsync();
        }

    }
}
