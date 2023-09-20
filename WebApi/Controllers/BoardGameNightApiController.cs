using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameNightApiController : ControllerBase
    {
        private readonly IBoardGameNightService _boardGameNightService;

        public BoardGameNightApiController(IBoardGameNightService boardGameNightService)
        {
            _boardGameNightService = boardGameNightService;
        }

        // GET: api/BoardGameNight
        [HttpGet]
        public async Task<IEnumerable<BoardGameNight>> GetBoardGameNights()
        {
            return await _boardGameNightService.GetAllBoardGameNightsAsync();
        }

        // GET: api/BoardGameNight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardGameNight>> GetBoardGameNight(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            return boardGameNight;
        }

        // POST: api/BoardGameNight
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BoardGameNight>> PostBoardGameNight(BoardGameNight boardGameNight)
        {
            try
            {
                // Create the BoardGameNight
                var createdBoardGameNight = await _boardGameNightService.CreateBoardGameNightAsync(boardGameNight);

                return CreatedAtAction("GetBoardGameNight", new { id = createdBoardGameNight.Id }, createdBoardGameNight);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/BoardGameNight/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardGameNight(int id, BoardGameNight boardGameNight)
        {
            var existingBoardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (existingBoardGameNight == null)
            {
                return NotFound();
            }

            await _boardGameNightService.UpdateBoardGameNightAsync(id, existingBoardGameNight);

            return NoContent();
        }

        // DELETE: api/BoardGameNight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardGameNight(int id)
        {
            var existingBoardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (existingBoardGameNight == null)
            {
                return NotFound();
            }

            await _boardGameNightService.DeleteBoardGameNightAsync(id);

            return NoContent();
        }
    }
}
