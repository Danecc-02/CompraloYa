using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompraloYa.Data;
using CompraloYa.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CompraloYa.Controllers
{
    public class OficinasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OficinasController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
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

            //return View(oficina);
            return RedirectToAction("Verificacion", "Oficinas", new { key2 = id});


        }

        // GET: Oficinas/Create
        public IActionResult Create()
        {
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName");
            return View();
        }

        // POST: Oficinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOficina,CodigoOficina,Nombre,DetalleOficina,Stock,PrecioOficina,IdSend,Img")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                //Guardar Imagen en wwwroot
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(oficina.Img.FileName);
                string extension = Path.GetExtension(oficina.Img.FileName);
                oficina.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await oficina.Img.CopyToAsync(fileStream);
                }
                //insert record

                _context.Add(oficina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName", oficina.IdSend);
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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName", oficina.IdSend);
            return View(oficina);
        }

        // POST: Oficinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOficina,CodigoOficina,Nombre,DetalleOficina,Stock,PrecioOficina,IdSend,Img")] Oficina oficina)
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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName", oficina.IdSend);
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

            //delete imagen from wwwroot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", oficina.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            //Delete the record

            _context.Oficinas.Remove(oficina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        private bool OficinaExists(int id)
        {
            return _context.Oficinas.Any(e => e.IdOficina == id);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult Verificacion()
        {
            return View();
        }

        //POST: Formualario
        [HttpPost]
        public async Task<IActionResult> Verificacion(string tarjetanum, int vcctarjeta, string fechaex, int idArtiOficina,int key2, string x, [Bind("IdTarjeta,NumeroTarjeta,FechaExpiracion,VCCtarjeta")] Tarjetas tarjetas)
        {


           




            var tarjetasV = await _context.Tarjetas.FindAsync(vcctarjeta);

            if (tarjetasV == null)
            {
                return RedirectToAction(nameof(ErrorTarjeta));
            }
            if (tarjetanum == tarjetasV.NumeroTarjeta && fechaex == tarjetasV.FechaExpiracion && vcctarjeta == tarjetasV.VCCtarjeta)
            {
                var oficina = await _context.Oficinas.FindAsync(key2);
                if (oficina.Stock > 1)
                {
                    oficina.Stock = oficina.Stock - 1;
                    _context.Update(oficina);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(TarjetaAceptada));

                }

                else if (oficina.Stock == 1)
                {
                    _context.Oficinas.Remove(oficina);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(TarjetaAceptada));

                }

               

            }
            else
            {
                return RedirectToAction(nameof(ErrorTarjeta));

            }


            return View();
        }
        public IActionResult ErrorTarjeta()
        {
            return View();
        }

        public IActionResult TarjetaAceptada()
        {
            return View();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

    

        /////////////////////////////////////////////////////////////////////////////////////////////
    }
}
