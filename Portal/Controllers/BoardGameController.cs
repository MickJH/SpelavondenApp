using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly IBoardGameService _boardGameService;

        public BoardGameController(IBoardGameService boardGameService)
        {
            _boardGameService = boardGameService;
        }

        [Authorize]
        // GET: BoardGame
        public async Task<IActionResult> Index()
        {
            var boardGames = await _boardGameService.GetAllBoardGamesAsync();
            return View(boardGames);
        }

        [Authorize]
        // GET: BoardGame/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoardGame/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGame boardGame)
        {
            if (ModelState.IsValid)
            {
                await _boardGameService.CreateBoardGameAsync(boardGame);
                return RedirectToAction(nameof(Index));
            }
            return View(boardGame);
        }

        // GET: BoardGame/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var boardGame = await _boardGameService.GetBoardGameByIdAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }
            return View(boardGame);
        }

        // POST: BoardGame/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoardGame boardGame)
        {
            if (id != boardGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _boardGameService.UpdateBoardGameAsync(id, boardGame);
                return RedirectToAction(nameof(Index));
            }
            return View(boardGame);
        }

        // GET: BoardGame/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var boardGame = await _boardGameService.GetBoardGameByIdAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }
            return View(boardGame);
        }

        // POST: BoardGame/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _boardGameService.DeleteBoardGameAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: BoardGame/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            // Fetch the BoardGame by id
            var boardGame = await _boardGameService.GetBoardGameByIdAsync(id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }


    }
}
