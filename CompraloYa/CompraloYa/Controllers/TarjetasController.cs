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
    public class TarjetasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarjetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tarjetas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarjetas.ToListAsync());
        }

        // GET: Tarjetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetas = await _context.Tarjetas
                .FirstOrDefaultAsync(m => m.VCCtarjeta == id);
            if (tarjetas == null)
            {
                return NotFound();
            }

            return View(tarjetas);
        }

        // GET: Tarjetas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarjetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VCCtarjeta,NumeroTarjeta,FechaExpiracion")] Tarjetas tarjetas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarjetas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarjetas);
        }

        // GET: Tarjetas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetas = await _context.Tarjetas.FindAsync(id);
            if (tarjetas == null)
            {
                return NotFound();
            }
            return View(tarjetas);
        }

        // POST: Tarjetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VCCtarjeta,NumeroTarjeta,FechaExpiracion")] Tarjetas tarjetas)
        {
            if (id != tarjetas.VCCtarjeta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarjetas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetasExists(tarjetas.VCCtarjeta))
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
            return View(tarjetas);
        }

        // GET: Tarjetas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetas = await _context.Tarjetas
                .FirstOrDefaultAsync(m => m.VCCtarjeta == id);
            if (tarjetas == null)
            {
                return NotFound();
            }

            return View(tarjetas);
        }

        // POST: Tarjetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarjetas = await _context.Tarjetas.FindAsync(id);
            _context.Tarjetas.Remove(tarjetas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetasExists(int id)
        {
            return _context.Tarjetas.Any(e => e.VCCtarjeta == id);
        }

        ////////////////////////////////////////CONFIRMAR PAGO/////////////////////////////////////////////////
        // GET: Formulario
        public ActionResult Verificacion()
        {
            return View();
        }

        //POST: Formualario
        [HttpPost]
        public async Task<IActionResult> Verificacion(string tarjetanum, int vcctarjeta, string fechaex, [Bind("IdTarjeta,NumeroTarjeta,FechaExpiracion,VCCtarjeta")] Tarjetas tarjetas )
        {

         

            var tarjetasV = await _context.Tarjetas.FindAsync(vcctarjeta);

            if (tarjetasV == null)
            {
                return Ok("Tarjeta denegada id no existe");
            }
            if (tarjetanum == tarjetasV.NumeroTarjeta && fechaex == tarjetasV.FechaExpiracion && vcctarjeta == tarjetasV.VCCtarjeta)
            {
                return Ok("SIII");

            }
            else
            {
                return Ok("datos incorrectos");

            }
            

            return View();
        }
    }
}
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    



