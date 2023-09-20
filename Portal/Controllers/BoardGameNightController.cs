
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class BoardGameNightController : Controller
    {
        private readonly IBoardGameNightService _boardGameNightService;
        private readonly UserManager<Person> _userManager;

        public BoardGameNightController(IBoardGameNightService boardGameNightService, UserManager<Person> userManager)
        {
            _boardGameNightService = boardGameNightService;
            _userManager = userManager;
        }

        // GET: BoardGameNight
        public async Task<IActionResult> Index()
        {
            var boardGameNights = await _boardGameNightService.GetAllBoardGameNightsAsync();
            return View(boardGameNights);
        }

        // GET: BoardGameNight/Details/5
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
        public IActionResult Create()
        {
            // Customize as needed
            return View();
        }

        // POST: BoardGameNight/Create
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

            if (ModelState.IsValid)
            {
                await _boardGameNightService.CreateBoardGameNightAsync(boardGameNight);
                return RedirectToAction(nameof(Index));
            }
            return View(boardGameNight);
        }

        // GET: BoardGameNight/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var boardGameNight = await _boardGameNightService.GetBoardGameNightByIdAsync(id);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            // Customize as needed
            return View(boardGameNight);
        }

        // POST: BoardGameNight/Edit/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _boardGameNightService.DeleteBoardGameNightAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
