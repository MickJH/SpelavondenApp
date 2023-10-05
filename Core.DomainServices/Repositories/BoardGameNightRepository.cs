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
            return await _context.BoardGameNights
                .ToListAsync();
        }


        public async Task<BoardGameNight> GetBoardGameNightByIdAsync(int id)
        {
            return await _context.BoardGameNights.FindAsync(id);
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



    }
}
