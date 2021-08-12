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
    public class TypeTecnologiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeTecnologiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeTecnologias
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeTecnologias.ToListAsync());
        }

        // GET: TypeTecnologias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeTecnologia = await _context.TypeTecnologias
                .FirstOrDefaultAsync(m => m.IdTypeTecno == id);
            if (typeTecnologia == null)
            {
                return NotFound();
            }

            return View(typeTecnologia);
        }

        // GET: TypeTecnologias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeTecnologias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTypeTecno,TypeTecnoName")] TypeTecnologia typeTecnologia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeTecnologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeTecnologia);
        }

        // GET: TypeTecnologias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeTecnologia = await _context.TypeTecnologias.FindAsync(id);
            if (typeTecnologia == null)
            {
                return NotFound();
            }
            return View(typeTecnologia);
        }

        // POST: TypeTecnologias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTypeTecno,TypeTecnoName")] TypeTecnologia typeTecnologia)
        {
            if (id != typeTecnologia.IdTypeTecno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeTecnologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeTecnologiaExists(typeTecnologia.IdTypeTecno))
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
            return View(typeTecnologia);
        }

        // GET: TypeTecnologias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeTecnologia = await _context.TypeTecnologias
                .FirstOrDefaultAsync(m => m.IdTypeTecno == id);
            if (typeTecnologia == null)
            {
                return NotFound();
            }

            return View(typeTecnologia);
        }

        // POST: TypeTecnologias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeTecnologia = await _context.TypeTecnologias.FindAsync(id);
            _context.TypeTecnologias.Remove(typeTecnologia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeTecnologiaExists(int id)
        {
            return _context.TypeTecnologias.Any(e => e.IdTypeTecno == id);
        }
    }
}
