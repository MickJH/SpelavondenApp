using Microsoft.AspNetCore.Mvc;
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[Route("api/[controller]")]
[ApiController]
public class BoardGameNightController : ControllerBase
{
    private readonly IBoardGameNightService _boardGameNightService;
    private readonly UserManager<Person> _userManager;

    public BoardGameNightController(IBoardGameNightService boardGameNightService, UserManager<Person> userManager)
    {
        _boardGameNightService = boardGameNightService;
        _userManager = userManager;
    }


    [Authorize]  // Require authentication to access this endpoint
    [HttpPost("join/{id}")]
    public async Task<IActionResult> JoinBoardGameNight(int id)
    {
        var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

        if (boardGameNight == null)
            return NotFound();

        // Get the current user
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser != null)
        {
            // Add the current user to the Players collection
            if (boardGameNight.Players == null)
                boardGameNight.Players = new List<Person>();

            boardGameNight.Players.Add(currentUser);
            await _boardGameNightService.UpdateBoardGameNightAsync(id, boardGameNight);

            return Ok();
        }

        return BadRequest("User not found.");
    }
}
