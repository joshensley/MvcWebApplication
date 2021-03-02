using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Data;
using MvcWebApplication.Models.ItemInformation.BoardGameInformation;
using MvcWebApplication.ViewModels.BoardGameCrud;

namespace MvcWebApplication.Controllers
{
    public class BoardGamesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public readonly IWebHostEnvironment _environment;

        public BoardGamesController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        // GET: BoardGames
        public async Task<IActionResult> Index(
            int? pageNumber, 
            string sortOrder,
            string searchString,
            string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            IQueryable<BoardGame> boardGamesIQ = _db.BoardGame
                .Include(b => b.BoardGameBrand);

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                boardGamesIQ = boardGamesIQ.Where(x => x.Title.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "title_desc":
                    boardGamesIQ = boardGamesIQ.OrderByDescending(x => x.Title);
                    break;
                default:
                    boardGamesIQ = boardGamesIQ.OrderBy(x => x.Title);
                    break;
            }


            int pageSize = 3;
            return View(await PaginatedList<BoardGame>.CreateAsync(boardGamesIQ.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: BoardGames/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _db.BoardGame
                .Include(b => b.BoardGameBrand)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // GET: BoardGames/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<BoardGameBrand> boardGameBrands = await _db.BoardGameBrand
                .OrderBy(x => x.BrandName)
                .ToListAsync();

            BoardGameCrudViewModel boardGameCrudViewModel = new BoardGameCrudViewModel()
            {
                BoardGame = new BoardGame() { ReleaseDate = DateTime.UtcNow.Date },
                BoardGameBrands = boardGameBrands
            };

            return View(boardGameCrudViewModel);
        }

        // POST: BoardGames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Controller,MaxPlayerNumber,MinPlayerNumber,BoardGameBrandID,ID,UPC,Title,Description,ReleaseDate,Price,ImageFilePath,FormFileUpload")] BoardGame boardGame)
        {
            if (ModelState.IsValid)
            {
                var filepath = "/images/boardgames/defaultBoardGame/Lead-Gray.jpg";
                if (boardGame.FormFileUpload != null)
                {
                    var rand = new Random();
                    var randNum = rand.Next(100000, 1000000);
                    var fileName = randNum.ToString() + "_" + boardGame.FormFileUpload.FileName;
                    filepath = $"/images/boardgames/{fileName}";
                    var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images/boardgames/", fileName);

                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await boardGame.FormFileUpload.CopyToAsync(fileStream);
                    }

                }

                var emptyboardGame = new BoardGame();

                if(await TryUpdateModelAsync<BoardGame>(
                    emptyboardGame,
                    "boardgame",
                    x => x.Type,
                    x => x.Controller,
                    x => x.MaxPlayerNumber,
                    x => x.MinPlayerNumber,
                    x => x.BoardGameBrandID,
                    x => x.UPC,
                    x => x.Title,
                    x => x.Description,
                    x => x.ReleaseDate,
                    x => x.Price,
                    x => x.ImageFilePath))
                {
                    emptyboardGame.ID = Guid.NewGuid();
                    emptyboardGame.ImageFilePath = filepath;
                    _db.Add(emptyboardGame);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            } 
            return View(boardGame);
        }

        // GET: BoardGames/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BoardGame boardGame = await _db.BoardGame.FindAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }

            IEnumerable<BoardGameBrand> boardGameBrands = await _db.BoardGameBrand
                .OrderBy(x => x.BrandName)
                .ToListAsync();

            BoardGameCrudViewModel boardGameCrudViewModel = new BoardGameCrudViewModel()
            {
                BoardGame = boardGame,
                BoardGameBrands = boardGameBrands
            };

            return View(boardGameCrudViewModel);
        }

        // POST: BoardGames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Type,Controller,MaxPlayerNumber,MinPlayerNumber,BoardGameBrandID,ID,UPC,Title,Description,ReleaseDate,Price,ImageFilePath,FormFileUpload")] BoardGame boardGame)
        {
            if (id != boardGame.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var boardGameToUpdate = await _db.BoardGame.FindAsync(id);

                    if (boardGameToUpdate == null)
                    {
                        return NotFound();
                    }

                    var filepath = boardGame.ImageFilePath;
                    var oldFilePath = boardGame.ImageFilePath;

                    if (boardGame.FormFileUpload != null)
                    {
                        var rand = new Random();
                        var randNum = rand.Next(100000, 1000000);
                        var fileName = randNum.ToString() + "_" + boardGame.FormFileUpload.FileName;
                        filepath = $"/images/boardgames/{fileName}";
                        var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images/boardgames/", fileName);

                        using (var fileStream = new FileStream(file, FileMode.Create))
                        {
                            await boardGame.FormFileUpload.CopyToAsync(fileStream);
                        }

                        // Will only delete if the path exists and if the path is not pointed to the default avatar image
                        var deleteFile = Path.Combine(_environment.ContentRootPath, $"wwwroot{boardGameToUpdate.ImageFilePath}");
                        if (System.IO.File.Exists(deleteFile) && (oldFilePath != "/images/boardgames/defaultBoardGame/Lead-Gray.jpg"))
                        {
                            System.IO.File.Delete(deleteFile);
                        }
                    }

                    if (await TryUpdateModelAsync<BoardGame>(
                        boardGameToUpdate,
                        "boardgame",
                        x => x.ID,
                        x => x.Type,
                        x => x.Controller,
                        x => x.MaxPlayerNumber,
                        x => x.MinPlayerNumber,
                        x => x.BoardGameBrandID,
                        x => x.UPC,
                        x => x.Title,
                        x => x.Description,
                        x => x.ReleaseDate,
                        x => x.Price,
                        x => x.ImageFilePath))
                    {
                        if (boardGame.FormFileUpload != null)
                        {
                            boardGameToUpdate.ImageFilePath = filepath;
                        }
                        await _db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardGameExists(boardGame.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(boardGame);
        }

        // GET: BoardGames/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BoardGame boardGame = await _db.BoardGame
                .Include(b => b.BoardGameBrand)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // POST: BoardGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _db.BoardGame.FindAsync(id);

            if (boardGame == null)
            {
                return NotFound();
            }

            _db.BoardGame.Remove(boardGame);

            var deleteFile = Path.Combine(_environment.ContentRootPath, $"wwwroot{boardGame.ImageFilePath}");

            if (System.IO.File.Exists(deleteFile) && (boardGame.ImageFilePath != "/images/boardgames/defaultBoardGame/Lead-Gray.jpg"))
            {
                System.IO.File.Delete(deleteFile);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BoardGameExists(Guid id)
        {
            return _db.BoardGame.Any(e => e.ID == id);
        }
    }
}
