using Microsoft.AspNetCore.Mvc;
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

[Route("api/boardgamenight")]
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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("join/{id}")]
    public async Task<IActionResult> JoinBoardGameNight(int id)
    {
        var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

        if (boardGameNight == null)
            return NotFound();

        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser != null)
        {
            boardGameNight.Players ??= new List<Player>();

            var userName = currentUser.UserName;

            if (boardGameNight.Players.Any(player => player.Name == userName))
            {
                return BadRequest("Unable to join: " + userName + " is already joined.");
            }

            if (boardGameNight.Players.Count >= boardGameNight.MaxPlayers)
            {
                return BadRequest("Unable to join: Maximum number of players has been reached.");
            }

            var player = new Player { Name = userName };
            boardGameNight.Players.Add(player);

            await _boardGameNightService.UpdateBoardGameNightAsync(id, boardGameNight);
            return Ok(userName + " joined the board game night.");
        }

        return BadRequest("User not found.");
    }


}
