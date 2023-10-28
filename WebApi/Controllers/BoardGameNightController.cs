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
using System.Text.Json;

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
        public async Task<IActionResult> JoinBoardGameNight(int id, [FromBody] SnackRequest snackRequest)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (boardGameNight == null)
                return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                if (boardGameNight.BringSnacks)
                {
                    if (snackRequest == null || snackRequest.Snacks == null || snackRequest.Snacks.Count == 0)
                    {
                        return BadRequest("Je moet tenminste 1 snack meenemen naar deze spelavond.");
                    }

                    var snacks = snackRequest.Snacks.Select(snackItem => new Snacks { Name = snackItem.Name }).ToList();

                    // Set snacks for the board game night
                    boardGameNight.Snacks = snacks;
                }



                int userAge = CalculateAge(currentUser.Birthdate);

                if (userAge < 18 && boardGameNight.Is18Plus || boardGameNight.IsOrganizerOverride18Plus)
                {
                    return BadRequest("Je mag niet deelnemen aan de bordspelavond omdat die 18+ is.");
                }

                if (boardGameNight.FoodAndDrinkOptions != null)
                {
                    string message = "Je neemt deel aan de bordspelavond";

                    if (currentUser.AvoidsAlcohol && !boardGameNight.FoodAndDrinkOptions.NonAlcoholic)
                    {
                        message += ", maar let op: Je hebt aangegeven geen alcohol te willen drinken, maar er zijn alleen dranken met alcohol beschikbaar op deze bordspelavond.\n";
                    }

                    if (currentUser.HasLactoseAllergy && !boardGameNight.FoodAndDrinkOptions.LactoseFree)
                    {
                        message += ", maar let op: Je hebt aangegeven een lactose-allergie te hebben, maar er zijn geen lactose vrije producten op deze bordspelavond.\n";
                    }

                    if (currentUser.HasNutAllergy && !boardGameNight.FoodAndDrinkOptions.NutFree)
                    {
                        message += ", maar let op: Je hebt aangegeven een notenallergie te hebben, maar er zijn geen producten zonder noten beschikbaar op deze bordspelavond.\n";
                    }

                    if (currentUser.IsVegetarian && !boardGameNight.FoodAndDrinkOptions.Vegetarian)
                    {
                        message += ", maar let op: Je hebt aangegeven vegetarisch te zijn, maar er zijn geen vegetarische opties beschikbaar op deze bordspelavond.\n";
                    }

                    // Check if the user is already registered for a board game night on the same date
                    boardGameNight.Players ??= new List<Player>();

                    var userName = currentUser.UserName;

                    var existingJoin = boardGameNight.Players.FirstOrDefault(p =>
                        p.Name == currentUser.UserName &&
                        p.JoinDateTime.Date == boardGameNight.DateAndTime?.Date);

                    if (existingJoin != null)
                    {
                        return BadRequest("Je bent al aangemeld voor een bordspelavond op deze datum.");
                    }

                    // Check if the maximum number of players is reached
                    if (boardGameNight.MaxPlayers.HasValue && boardGameNight.Players.Count >= boardGameNight.MaxPlayers)
                    {
                        return BadRequest("Het maximum aantal spelers voor deze bordspelavond is bereikt.");
                    }

                    // Set the JoinDateTime for the player
                    var player = new Player { Name = currentUser.UserName, JoinDateTime = (DateTime)boardGameNight.DateAndTime! };

                    // Add the player to the board game night
                    boardGameNight.Players ??= new List<Player>();
                    boardGameNight.Players.Add(player);

                    // Update the board game night
                    await _boardGameNightService.UpdateBoardGameNightAsync(id, boardGameNight);

                    return Ok(message);
                }
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
