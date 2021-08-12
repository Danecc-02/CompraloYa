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
    public class TypeSendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeSendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeSends
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeSends.ToListAsync());
        }

        // GET: TypeSends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeSend = await _context.TypeSends
                .FirstOrDefaultAsync(m => m.IdSend == id);
            if (typeSend == null)
            {
                return NotFound();
            }

            return View(typeSend);
        }

        // GET: TypeSends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeSends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSend,TypeSendName")] TypeSend typeSend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeSend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeSend);
        }

        // GET: TypeSends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeSend = await _context.TypeSends.FindAsync(id);
            if (typeSend == null)
            {
                return NotFound();
            }
            return View(typeSend);
        }

        // POST: TypeSends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSend,TypeSendName")] TypeSend typeSend)
        {
            if (id != typeSend.IdSend)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeSend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeSendExists(typeSend.IdSend))
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
            return View(typeSend);
        }

        // GET: TypeSends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeSend = await _context.TypeSends
                .FirstOrDefaultAsync(m => m.IdSend == id);
            if (typeSend == null)
            {
                return NotFound();
            }

            return View(typeSend);
        }

        // POST: TypeSends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeSend = await _context.TypeSends.FindAsync(id);
            _context.TypeSends.Remove(typeSend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeSendExists(int id)
        {
            return _context.TypeSends.Any(e => e.IdSend == id);
        }
    }
}
