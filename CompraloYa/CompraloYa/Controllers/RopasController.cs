using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompraloYa.Data;
using CompraloYa.Models;

namespace CompraloYa.Controllers
{
    public class RopasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RopasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ropas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ropas.Include(r => r.TypeRopa).Include(r => r.TypeSend);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ropas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropas
                .Include(r => r.TypeRopa)
                .Include(r => r.TypeSend)
                .FirstOrDefaultAsync(m => m.IdTypeRopa == id);
            if (ropa == null)
            {
                return NotFound();
            }

            return View(ropa);
        }

        // GET: Ropas/Create
        public IActionResult Create()
        {
            ViewData["IdRopa"] = new SelectList(_context.TypeRopas, "IdTypeRopa", "IdTypeRopa");
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend");
            return View();
        }

        // POST: Ropas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTypeRopa,CodigoRopa,Detalle,Stock,PrecioRopa,IdRopa,IdSend")] Ropa ropa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ropa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRopa"] = new SelectList(_context.TypeRopas, "IdTypeRopa", "IdTypeRopa", ropa.IdRopa);
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", ropa.IdSend);
            return View(ropa);
        }

        // GET: Ropas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropas.FindAsync(id);
            if (ropa == null)
            {
                return NotFound();
            }
            ViewData["IdRopa"] = new SelectList(_context.TypeRopas, "IdTypeRopa", "IdTypeRopa", ropa.IdRopa);
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", ropa.IdSend);
            return View(ropa);
        }

        // POST: Ropas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTypeRopa,CodigoRopa,Detalle,Stock,PrecioRopa,IdRopa,IdSend")] Ropa ropa)
        {
            if (id != ropa.IdTypeRopa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ropa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RopaExists(ropa.IdTypeRopa))
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
            ViewData["IdRopa"] = new SelectList(_context.TypeRopas, "IdTypeRopa", "IdTypeRopa", ropa.IdRopa);
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", ropa.IdSend);
            return View(ropa);
        }

        // GET: Ropas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropas
                .Include(r => r.TypeRopa)
                .Include(r => r.TypeSend)
                .FirstOrDefaultAsync(m => m.IdTypeRopa == id);
            if (ropa == null)
            {
                return NotFound();
            }

            return View(ropa);
        }

        // POST: Ropas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ropa = await _context.Ropas.FindAsync(id);
            _context.Ropas.Remove(ropa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RopaExists(int id)
        {
            return _context.Ropas.Any(e => e.IdTypeRopa == id);
        }
    }
}
