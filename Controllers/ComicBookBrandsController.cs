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
    public class ComicBookBrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComicBookBrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComicBookBrands
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComicBookBrand.ToListAsync());
        }

        // GET: ComicBookBrands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBookBrand = await _context.ComicBookBrand
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comicBookBrand == null)
            {
                return NotFound();
            }

            return View(comicBookBrand);
        }

        // GET: ComicBookBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComicBookBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BrandName")] ComicBookBrand comicBookBrand)
        {
            if (ModelState.IsValid)
            {
                comicBookBrand.ID = Guid.NewGuid();
                _context.Add(comicBookBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBookBrand);
        }

        // GET: ComicBookBrands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBookBrand = await _context.ComicBookBrand.FindAsync(id);
            if (comicBookBrand == null)
            {
                return NotFound();
            }
            return View(comicBookBrand);
        }

        // POST: ComicBookBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,BrandName")] ComicBookBrand comicBookBrand)
        {
            if (id != comicBookBrand.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBookBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookBrandExists(comicBookBrand.ID))
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
            return View(comicBookBrand);
        }

        // GET: ComicBookBrands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBookBrand = await _context.ComicBookBrand
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comicBookBrand == null)
            {
                return NotFound();
            }

            return View(comicBookBrand);
        }

        // POST: ComicBookBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comicBookBrand = await _context.ComicBookBrand.FindAsync(id);
            _context.ComicBookBrand.Remove(comicBookBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicBookBrandExists(Guid id)
        {
            return _context.ComicBookBrand.Any(e => e.ID == id);
        }
    }
}
