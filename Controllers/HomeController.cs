using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcWebApplication.Data;
using MvcWebApplication.Models;
using MvcWebApplication.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index(
            int? pageNumber,
            string searchString,
            string currentFilter,
            string sortOrder,
            string itemTypeComicBook,
            string itemTypeBoardGame)
        {

            // Sort Title
            ViewData["CurrentSort"] = sortOrder ?? "";
            ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_descending" : "";

            // Filter Item Type
            ViewData["CurrentItemTypeComicBook"] = itemTypeComicBook;
            ViewData["CurrentItemTypeBoardGame"] = itemTypeBoardGame;
            ViewData["ItemTypeComicBook"] = String.IsNullOrEmpty(itemTypeComicBook) ? false : true;
            ViewData["ItemTypeBoardGame"] = String.IsNullOrEmpty(itemTypeBoardGame) ? false : true;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;

            var unionItemsIQ =
                (from boardgames in _db.BoardGame
                 select new UnionItems()
                 {
                     ID = boardgames.ID,
                     Type = boardgames.Type,
                     Controller = boardgames.Controller,
                     UPC = boardgames.UPC,
                     Title = boardgames.Title,
                     Description = boardgames.Description,
                     ReleaseDate = boardgames.ReleaseDate,
                     Price = boardgames.Price,
                     ImageFilePath = boardgames.ImageFilePath
                 })
                .Union
                    (from comicbooks in _db.ComicBook
                     select new UnionItems()
                     {
                         ID = comicbooks.ID,
                         Type = comicbooks.Type,
                         Controller = comicbooks.Controller,
                         UPC = comicbooks.UPC,
                         Title = comicbooks.Title,
                         Description = comicbooks.Description,
                         ReleaseDate = comicbooks.ReleaseDate,
                         Price = comicbooks.Price,
                         ImageFilePath = comicbooks.ImageFilePath
                     });

            // Search Input
            if (!String.IsNullOrEmpty(searchString))
            {
                unionItemsIQ = unionItemsIQ.Where(x => x.Title.Contains(searchString));
            }

            // Filter Item Type
            var boardGameType = String.IsNullOrEmpty(itemTypeBoardGame) ? "" : "Board Game";
            var comicBookType = String.IsNullOrEmpty(itemTypeComicBook) ? "" : "Comic Book";
            if (!String.IsNullOrEmpty(itemTypeBoardGame) ||
                !String.IsNullOrEmpty(itemTypeComicBook))
            {
                unionItemsIQ = unionItemsIQ.Where(x => x.Type == boardGameType || 
                                                        x.Type == comicBookType);
            }

            // Sort order 
            switch(sortOrder)
            {
                case "title_descending":
                    unionItemsIQ = unionItemsIQ.OrderByDescending(x => x.Title);
                    break;
                default:
                    unionItemsIQ = unionItemsIQ.OrderBy(x => x.Title);
                    break;
            }

            int pageSize = 3;
            PaginatedList<UnionItems> unionItems = await PaginatedList<UnionItems>.CreateAsync(unionItemsIQ.AsNoTracking(), pageNumber ?? 1, pageSize);

            HomeViewModel HomeViewModel = new HomeViewModel()
            {
                UnionItems = unionItems
            };

            return View(HomeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
