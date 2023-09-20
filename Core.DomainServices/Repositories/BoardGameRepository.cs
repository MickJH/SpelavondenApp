using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using Core.DomainServices.Repositories.Interfaces;
using Infrastructure;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DomainServices.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly ApplicationDbContext _context;

        public BoardGameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync()
        {
            return await _context.BoardGames.ToListAsync();
        }

        public async Task<BoardGame> GetBoardGameByIdAsync(int id)
        {
            return await _context.BoardGames.FindAsync(id);
        }

        public async Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame)
        {
            _context.BoardGames.Add(boardGame);
            await _context.SaveChangesAsync();
            return boardGame;
        }

        public async Task UpdateBoardGameAsync(BoardGame boardGame)
        {
            _context.Entry(boardGame).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoardGameAsync(int id)
        {
            var boardGame = await _context.BoardGames.FindAsync(id);
            if (boardGame != null)
            {
                _context.BoardGames.Remove(boardGame);
                await _context.SaveChangesAsync();
            }
        }
    }
}
