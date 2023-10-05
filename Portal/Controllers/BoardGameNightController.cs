
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portal.Controllers
{
    public class BoardGameNightController : Controller
    {
        private readonly IBoardGameNightService _boardGameNightService;
        private readonly IBoardGameService _boardGameService;
        private readonly UserManager<Person> _userManager;

        public BoardGameNightController(IBoardGameNightService boardGameNightService, UserManager<Person> userManager, IBoardGameService boardGameService)
        {
            _boardGameNightService = boardGameNightService;
            _userManager = userManager;
            _boardGameService = boardGameService;
        }

        // GET: BoardGameNight
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var boardGameNights = await _boardGameNightService.GetAllBoardGameNightsAsync();

            // Populate the selected games for each board game night
            var selectedGames = new List<BoardGame>();
            foreach (var night in boardGameNights)
            {
                var selectedGame = await _boardGameService.GetBoardGameByIdAsync(night.SelectedBoardGameId ?? 0);

                // Ensure the selected game is unique
                if (selectedGame != null && !selectedGames.Any(game => game.Id == selectedGame.Id))
                {
                    selectedGames.Add(selectedGame);
                }
            }

            ViewBag.SelectedGames = selectedGames;

            return View(boardGameNights);
        }

        //Get: BoardGameNight/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var boardGames = await _boardGameService.GetAllBoardGamesAsync();

            var model = new BoardGameNight
            {
                Games = boardGames.ToList() // Populate the list of games
            };

            return View(model);
        }

        // POST: BoardGameNight/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGameNight boardGameNight, int selectedBoardGameId)
        {
            var user = await _userManager.GetUserAsync(User);
            SetOrganizerValues(boardGameNight, user);
            SetFoodAndDrinkOptions(boardGameNight);

            // Get the list of games again
            boardGameNight.Games = (ICollection<BoardGame>?)await _boardGameService.GetAllBoardGamesAsync();

            if (ModelState.IsValid)
            {
                await _boardGameNightService.CreateBoardGameNightAsync(boardGameNight);
                return RedirectToAction(nameof(Index));
            }

            return View(boardGameNight);
        }


        private void SetOrganizerValues(BoardGameNight boardGameNight, Person user)
        {
            if (user != null)
            {
                boardGameNight.OrganizerName = user.Name;
                boardGameNight.OrganizerId = user.Id;
            }
        }

        private void SetFoodAndDrinkOptions(BoardGameNight boardGameNight)
        {
            var foodAndDrinkOptions = new FoodAndDrinkOption
            {
                LactoseFree = Request.Form["FoodAndDrinkOptions.LactoseFree"] == "on",
                NutFree = Request.Form["FoodAndDrinkOptions.NutFree"] == "on",
                Vegetarian = Request.Form["FoodAndDrinkOptions.Vegetarian"] == "on",
                NonAlcoholic = Request.Form["FoodAndDrinkOptions.NonAlcoholic"] == "on"
            };

            boardGameNight.FoodAndDrinkOptions = foodAndDrinkOptions;
        }




        // GET: BoardGameNight/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            // Check if the current user is the organizer of this BoardGameNight
            var user = await _userManager.GetUserAsync(User);
            if (boardGameNight != null && boardGameNight.OrganizerId != user.Id)
            {
                // User is not the organizer, return an error or redirect
                return Forbid(); // You can customize this behavior as needed
            }

            if (boardGameNight == null)
            {
                return NotFound();
            }

            return View(boardGameNight);
        }


        // POST: BoardGameNight/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoardGameNight boardGameNight)
        {
            if (id != boardGameNight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _boardGameNightService.UpdateBoardGameNightAsync(id, boardGameNight);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(boardGameNight);
        }

        // GET: BoardGameNight/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            return View(boardGameNight);
        }

        // POST: BoardGameNight/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _boardGameNightService.DeleteBoardGameNightAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: BoardGameNight/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id.Value);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            // Get the selected board game based on the SelectedBoardGameId
            var selectedGame = await _boardGameService.GetBoardGameByIdAsync((int)boardGameNight.SelectedBoardGameId!);

            ViewBag.SelectedGame = selectedGame;

            return View(boardGameNight);
        }

    }
}
