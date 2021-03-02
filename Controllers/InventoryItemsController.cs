using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Data;
using MvcWebApplication.Models.ItemInformation;

namespace MvcWebApplication.Controllers
{
    public class InventoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InventoryItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventoryItem.Include(i => i.BoardGame).Include(i => i.ComicBook);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InventoryItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem
                .Include(i => i.BoardGame)
                .Include(i => i.ComicBook)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // GET: InventoryItems/Create
        public IActionResult Create()
        {
            ViewData["BoardGameID"] = new SelectList(_context.BoardGame, "ID", "Controller");
            ViewData["ComicBookID"] = new SelectList(_context.ComicBook, "ID", "Controller");
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InStock,ReceivedNumber,ReceivedDate,OrderNumber,OrderedDate,ShippingNumber,ShippedDate,BoardGameID,ComicBookID")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                inventoryItem.ID = Guid.NewGuid();
                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardGameID"] = new SelectList(_context.BoardGame, "ID", "Controller", inventoryItem.BoardGameID);
            ViewData["ComicBookID"] = new SelectList(_context.ComicBook, "ID", "Controller", inventoryItem.ComicBookID);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            ViewData["BoardGameID"] = new SelectList(_context.BoardGame, "ID", "Controller", inventoryItem.BoardGameID);
            ViewData["ComicBookID"] = new SelectList(_context.ComicBook, "ID", "Controller", inventoryItem.ComicBookID);
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,InStock,ReceivedNumber,ReceivedDate,OrderNumber,OrderedDate,ShippingNumber,ShippedDate,BoardGameID,ComicBookID")] InventoryItem inventoryItem)
        {
            if (id != inventoryItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemExists(inventoryItem.ID))
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
            ViewData["BoardGameID"] = new SelectList(_context.BoardGame, "ID", "Controller", inventoryItem.BoardGameID);
            ViewData["ComicBookID"] = new SelectList(_context.ComicBook, "ID", "Controller", inventoryItem.ComicBookID);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem
                .Include(i => i.BoardGame)
                .Include(i => i.ComicBook)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // POST: InventoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var inventoryItem = await _context.InventoryItem.FindAsync(id);
            _context.InventoryItem.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryItemExists(Guid id)
        {
            return _context.InventoryItem.Any(e => e.ID == id);
        }
    }
}
