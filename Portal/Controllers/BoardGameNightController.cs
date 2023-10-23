
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Models;

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

        [Authorize]
        // GET: BoardGameNight
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> AllBoardGameNights()
        {
            var boardGameNights = await _boardGameNightService.GetAllBoardGameNightsAsync();

            var selectedGameIds = boardGameNights.Select(night => night.SelectedBoardGameId).ToList();

            ViewBag.SelectedGameIds = selectedGameIds;

            return View(boardGameNights);
        }


        [Authorize]
        public async Task<IActionResult> OrganizedBoardGameNights()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if the user is not authenticated
            }

            // Retrieve board game nights organized by the logged-in user
            var organizedBoardGameNights = await _boardGameNightService.GetBoardGameNightsByOrganizerAsync(currentUser.UserName!);

            var boardGameNights = await _boardGameNightService.GetAllBoardGameNightsAsync();

            var selectedGameIds = boardGameNights.Select(night => night.SelectedBoardGameId).ToList();

            ViewBag.SelectedGameIds = selectedGameIds;

            return View(organizedBoardGameNights);
        }


        [Authorize]
        public async Task<IActionResult> JoinedBoardGameNights()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve board game nights where the logged-in user is a participant
            var joinedBoardGameNights = await _boardGameNightService.GetBoardGameNightsByParticipantAsync(currentUser.UserName);

            var boardGameNights = await _boardGameNightService.GetAllBoardGameNightsAsync();

            var selectedGameIds = boardGameNights.Select(night => night.SelectedBoardGameId).ToList();

            ViewBag.SelectedGameIds = selectedGameIds;

            return View(joinedBoardGameNights);
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

            // Get the list of games again
            boardGameNight.Games = (ICollection<BoardGame>?)await _boardGameService.GetAllBoardGamesAsync();

            // Get the selected board game
            var selectedGame = await _boardGameService.GetBoardGameByIdAsync(selectedBoardGameId);



            if (selectedGame == null)
            {
                ModelState.AddModelError("", "Selecteer een spel.");  // Add appropriate error message
                return View(boardGameNight);
            }
            else
            {
                boardGameNight.SelectedBoardGame = selectedGame;
            }

            // Check if the selected board game is 18+ or if the organizer has overridden the 18+ status
            boardGameNight.Is18Plus = selectedGame.Is18Plus || boardGameNight.IsOrganizerOverride18Plus;

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
                boardGameNight.OrganizerName = user.UserName;
                boardGameNight.OrganizerId = user.Id;
            }
        }

        // GET: BoardGameNight/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            var user = await _userManager.GetUserAsync(User);
            if (boardGameNight != null && boardGameNight.OrganizerName != user.UserName)
            {
                TempData["Message"] = "Je mag alleen jouw eigen bordspelavonden bewerken!";
                return RedirectToAction(nameof(AllBoardGameNights));
            }

            if (boardGameNight == null)
            {
                return NotFound();
            }

            // Fetch the list of board games again
            var boardGames = await _boardGameService.GetAllBoardGamesAsync();

            // Populate the list of games for the boardGameNight
            boardGameNight.Games = boardGames.ToList();

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

            var user = await _userManager.GetUserAsync(User);
            var existingBoardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (existingBoardGameNight == null)
            {
                return NotFound();
            }

            if (existingBoardGameNight.OrganizerName != user.UserName)
            {
                TempData["Message"] = "Je mag alleen jouw eigen bordspelavonden bewerken!";
                return RedirectToAction(nameof(AllBoardGameNights));
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

            // Fetch the list of board games again
            var boardGames = await _boardGameService.GetAllBoardGamesAsync();

            // Populate the list of games for the boardGameNight
            boardGameNight.Games = boardGames.ToList();

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

            var user = await _userManager.GetUserAsync(User);
            if (boardGameNight.OrganizerName != user.UserName)
            {
                TempData["Message"] = "Je mag alleen jouw eigen bordspelavond verwijderen!";
                return RedirectToAction(nameof(AllBoardGameNights));
            }

            return View(boardGameNight);
        }

        // POST: BoardGameNight/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (boardGameNight.OrganizerName != user.UserName)
            {
                TempData["Message"] = "Je mag alleen jouw eigen bordpselavond verwijderen!";
                return RedirectToAction(nameof(AllBoardGameNights));
            }

            if (boardGameNight.Players.Count() > 0)
            {
                TempData["Message"] = "Je kunt een bordspelavond niet verwijderen als er al spelers zijn aangemeld!";
                return RedirectToAction(nameof(AllBoardGameNights));
            }

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
            var selectedBoardGame = await _boardGameService.GetBoardGameByIdAsync((int)boardGameNight.SelectedBoardGameId!);

            ViewBag.SelectedGame = selectedBoardGame;

            return View(boardGameNight);
        }

    }
}
