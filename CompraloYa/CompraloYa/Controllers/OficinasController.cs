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
    public class OficinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OficinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Oficinas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Oficinas.Include(o => o.TypeSend);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Oficinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficina = await _context.Oficinas
                .Include(o => o.TypeSend)
                .FirstOrDefaultAsync(m => m.IdOficina == id);
            if (oficina == null)
            {
                return NotFound();
            }

            return View(oficina);
        }

        // GET: Oficinas/Create
        public IActionResult Create()
        {
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend");
            return View();
        }

        // POST: Oficinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOficina,CodigoOficina,Nombre,DetalleOficina,Stock,PrecioOficina,IdSend")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oficina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", oficina.IdSend);
            return View(oficina);
        }

        // GET: Oficinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficina = await _context.Oficinas.FindAsync(id);
            if (oficina == null)
            {
                return NotFound();
            }
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", oficina.IdSend);
            return View(oficina);
        }

        // POST: Oficinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOficina,CodigoOficina,Nombre,DetalleOficina,Stock,PrecioOficina,IdSend")] Oficina oficina)
        {
            if (id != oficina.IdOficina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oficina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OficinaExists(oficina.IdOficina))
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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", oficina.IdSend);
            return View(oficina);
        }

        // GET: Oficinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficina = await _context.Oficinas
                .Include(o => o.TypeSend)
                .FirstOrDefaultAsync(m => m.IdOficina == id);
            if (oficina == null)
            {
                return NotFound();
            }

            return View(oficina);
        }

        // POST: Oficinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oficina = await _context.Oficinas.FindAsync(id);
            _context.Oficinas.Remove(oficina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OficinaExists(int id)
        {
            return _context.Oficinas.Any(e => e.IdOficina == id);
        }
    }
}
