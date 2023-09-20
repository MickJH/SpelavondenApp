
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            return View(boardGameNights);
        }

        // GET: BoardGameNight/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            return View(boardGameNight);
        }

        // GET: BoardGameNight/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoardGameNight/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGameNight boardGameNight)
        {
            //Get the current user
            var user = await _userManager.GetUserAsync(User);

            //Set organizer values
            if (user != null)
            {
                boardGameNight.OrganizerName = user.Name;
                boardGameNight.OrganizerId = user.Id;
            }

            var boardGames = await _boardGameService.GetAllBoardGamesAsync();

            if (boardGames != null)
            {
                boardGameNight.Games = boardGames.ToList();
            }
            else
            {
                // Handle the case where boardGames is null, e.g., assign an empty list
                boardGameNight.Games = new List<BoardGame>();
            }

            if (ModelState.IsValid)
            {
                await _boardGameNightService.CreateBoardGameNightAsync(boardGameNight);
                return RedirectToAction(nameof(Index));
            }
            return View(boardGameNight);
        }


        // GET: BoardGameNight/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

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
    }
}
