using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Data;
using MvcWebApplication.Models.ItemInformation.ComicBookInformation;

namespace MvcWebApplication.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComicBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComicBook.Include(c => c.ComicBookBrand);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBook
                .Include(c => c.ComicBookBrand)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            ViewData["ComicBookBrandId"] = new SelectList(_context.ComicBookBrand, "ID", "BrandName");
            return View();
        }

        // POST: ComicBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Controller,Pages,ComicBookBrandId,ID,UPC,Title,Description,ReleaseDate,Price,ImageFilePath")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                comicBook.ID = Guid.NewGuid();
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComicBookBrandId"] = new SelectList(_context.ComicBookBrand, "ID", "BrandName", comicBook.ComicBookBrandId);
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBook.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            ViewData["ComicBookBrandId"] = new SelectList(_context.ComicBookBrand, "ID", "BrandName", comicBook.ComicBookBrandId);
            return View(comicBook);
        }

        // POST: ComicBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Type,Controller,Pages,ComicBookBrandId,ID,UPC,Title,Description,ReleaseDate,Price,ImageFilePath")] ComicBook comicBook)
        {
            if (id != comicBook.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookExists(comicBook.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComicBookBrandId"] = new SelectList(_context.ComicBookBrand, "ID", "BrandName", comicBook.ComicBookBrandId);
            return View(comicBook);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBook
                .Include(c => c.ComicBookBrand)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comicBook = await _context.ComicBook.FindAsync(id);
            _context.ComicBook.Remove(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicBookExists(Guid id)
        {
            return _context.ComicBook.Any(e => e.ID == id);
        }
    }
}
