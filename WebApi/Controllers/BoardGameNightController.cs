using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                int userAge = CalculateAge(currentUser.Birthdate);

                if (userAge < 18 && boardGameNight.Is18Plus || boardGameNight.IsOrganizerOverride18Plus)
                {
                    return BadRequest("Je mag niet deelnemen aan de bordspelavond omdat die 18+ is.");
                }

                boardGameNight.Players ??= new List<Player>();

                var userName = currentUser.UserName;

                if (boardGameNight.Players.Any(player => player.Name == userName))
                {
                    return BadRequest("Kan niet deelnemen: " + userName + " doet al mee aan de spelavond.");
                }

                if (boardGameNight.Players.Count >= boardGameNight.MaxPlayers)
                {
                    return BadRequest("Kan niet deelnemen: Maximum aantal spelers voor de spelavond is bereikt.");
                }

                var player = new Player { Name = userName };
                boardGameNight.Players.Add(player);

                await _boardGameNightService.UpdateBoardGameNightAsync(id, boardGameNight);
                return Ok(userName + " neemt deel aan de borspelavond.");
            }

            return BadRequest("Gebruiker niet gevonden.");
        }

        private int CalculateAge(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
                return 0;

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Value.Year;
            if (dateOfBirth.Value.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
