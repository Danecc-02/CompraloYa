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
    public class TypeRopasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeRopasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeRopas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeRopas.ToListAsync());
        }

        // GET: TypeRopas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRopa = await _context.TypeRopas
                .FirstOrDefaultAsync(m => m.IdTypeRopa == id);
            if (typeRopa == null)
            {
                return NotFound();
            }

            return View(typeRopa);
        }

        // GET: TypeRopas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeRopas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTypeRopa,TypeRopaName")] TypeRopa typeRopa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeRopa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeRopa);
        }

        // GET: TypeRopas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRopa = await _context.TypeRopas.FindAsync(id);
            if (typeRopa == null)
            {
                return NotFound();
            }
            return View(typeRopa);
        }

        // POST: TypeRopas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTypeRopa,TypeRopaName")] TypeRopa typeRopa)
        {
            if (id != typeRopa.IdTypeRopa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeRopa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeRopaExists(typeRopa.IdTypeRopa))
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
            return View(typeRopa);
        }

        // GET: TypeRopas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRopa = await _context.TypeRopas
                .FirstOrDefaultAsync(m => m.IdTypeRopa == id);
            if (typeRopa == null)
            {
                return NotFound();
            }

            return View(typeRopa);
        }

        // POST: TypeRopas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeRopa = await _context.TypeRopas.FindAsync(id);
            _context.TypeRopas.Remove(typeRopa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeRopaExists(int id)
        {
            return _context.TypeRopas.Any(e => e.IdTypeRopa == id);
        }
    }
}
