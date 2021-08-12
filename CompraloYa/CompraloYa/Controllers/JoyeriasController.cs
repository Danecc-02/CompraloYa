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
    public class JoyeriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JoyeriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Joyerias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Joyerias.Include(j => j.TypeSend);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Joyerias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joyeria = await _context.Joyerias
                .Include(j => j.TypeSend)
                .FirstOrDefaultAsync(m => m.IdJoyeria == id);
            if (joyeria == null)
            {
                return NotFound();
            }

            return View(joyeria);
        }

        // GET: Joyerias/Create
        public IActionResult Create()
        {
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend");
            return View();
        }

        // POST: Joyerias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJoyeria,CodigoJoya,NombreJoya,DetalleJoya,Stock,PrecioJoya,IdSend")] Joyeria joyeria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joyeria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", joyeria.IdSend);
            return View(joyeria);
        }

        // GET: Joyerias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joyeria = await _context.Joyerias.FindAsync(id);
            if (joyeria == null)
            {
                return NotFound();
            }
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", joyeria.IdSend);
            return View(joyeria);
        }

        // POST: Joyerias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJoyeria,CodigoJoya,NombreJoya,DetalleJoya,Stock,PrecioJoya,IdSend")] Joyeria joyeria)
        {
            if (id != joyeria.IdJoyeria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joyeria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoyeriaExists(joyeria.IdJoyeria))
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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "IdSend", joyeria.IdSend);
            return View(joyeria);
        }

        // GET: Joyerias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joyeria = await _context.Joyerias
                .Include(j => j.TypeSend)
                .FirstOrDefaultAsync(m => m.IdJoyeria == id);
            if (joyeria == null)
            {
                return NotFound();
            }

            return View(joyeria);
        }

        // POST: Joyerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var joyeria = await _context.Joyerias.FindAsync(id);
            _context.Joyerias.Remove(joyeria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JoyeriaExists(int id)
        {
            return _context.Joyerias.Any(e => e.IdJoyeria == id);
        }
    }
}
