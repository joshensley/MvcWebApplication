using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Data;
using MvcWebApplication.Models.ItemInformation.BoardGameInformation;

namespace MvcWebApplication.Controllers
{
    public class BoardGameBrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardGameBrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BoardGameBrands
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoardGameBrand.ToListAsync());
        }

        // GET: BoardGameBrands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGameBrand = await _context.BoardGameBrand
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boardGameBrand == null)
            {
                return NotFound();
            }

            return View(boardGameBrand);
        }

        // GET: BoardGameBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoardGameBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BrandName")] BoardGameBrand boardGameBrand)
        {
            if (ModelState.IsValid)
            {
                boardGameBrand.ID = Guid.NewGuid();
                _context.Add(boardGameBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boardGameBrand);
        }

        // GET: BoardGameBrands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGameBrand = await _context.BoardGameBrand.FindAsync(id);
            if (boardGameBrand == null)
            {
                return NotFound();
            }
            return View(boardGameBrand);
        }

        // POST: BoardGameBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,BrandName")] BoardGameBrand boardGameBrand)
        {
            if (id != boardGameBrand.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardGameBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardGameBrandExists(boardGameBrand.ID))
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
            return View(boardGameBrand);
        }

        // GET: BoardGameBrands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGameBrand = await _context.BoardGameBrand
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boardGameBrand == null)
            {
                return NotFound();
            }

            return View(boardGameBrand);
        }

        // POST: BoardGameBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var boardGameBrand = await _context.BoardGameBrand.FindAsync(id);
            _context.BoardGameBrand.Remove(boardGameBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardGameBrandExists(Guid id)
        {
            return _context.BoardGameBrand.Any(e => e.ID == id);
        }
    }
}
