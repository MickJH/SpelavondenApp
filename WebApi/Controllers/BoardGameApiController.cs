using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameApiController : ControllerBase
    {
        private readonly IBoardGameService _boardGameService;

        public BoardGameApiController(IBoardGameService boardGameService)
        {
            _boardGameService = boardGameService;
        }

        // GET: api/BoardGame
        [HttpGet]
        public async Task<IEnumerable<BoardGame>> GetBoardGames()
        {
            return await _boardGameService.GetAllBoardGamesAsync();
        }

        // GET: api/BoardGame/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardGame>> GetBoardGame(int id)
        {
            var boardGame = await _boardGameService.GetBoardGameByIdAsync(id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return boardGame;
        }

        // POST: api/BoardGame
        [HttpPost]
        public async Task<ActionResult<BoardGame>> PostBoardGame(BoardGame boardGame)
        {
            var createdBoardGame = await _boardGameService.CreateBoardGameAsync(boardGame);

            return CreatedAtAction("GetBoardGame", new { id = createdBoardGame.Id }, createdBoardGame);
        }

        // PUT: api/BoardGame/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardGame(int id, BoardGame boardGame)
        {
            var existingBoardGame = await _boardGameService.GetBoardGameByIdAsync(id);

            if (existingBoardGame == null)
            {
                return NotFound();
            }

            // Update the boardGame properties
            existingBoardGame.Name = boardGame.Name;
            existingBoardGame.Description = boardGame.Description;
            existingBoardGame.Genre = boardGame.Genre;
            existingBoardGame.Is18Plus = boardGame.Is18Plus;
            existingBoardGame.PhotoUrl = boardGame.PhotoUrl;
            existingBoardGame.GameType = boardGame.GameType;

            await _boardGameService.UpdateBoardGameAsync(id, existingBoardGame);

            return NoContent();
        }

        // DELETE: api/BoardGame/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardGame(int id)
        {
            var existingBoardGame = await _boardGameService.GetBoardGameByIdAsync(id);

            if (existingBoardGame == null)
            {
                return NotFound();
            }

            await _boardGameService.DeleteBoardGameAsync(id);

            return NoContent();
        }
    }
}
