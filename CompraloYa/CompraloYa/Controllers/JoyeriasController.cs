using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompraloYa.Data;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CompraloYa.Common;

namespace CompraloYa.Models
{
    public class JoyeriasController : Controller
    {
        private readonly int RecordsPerPage = 10;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Pagination<Joyeria> PaginationJoyeria { get; private set; }

        public JoyeriasController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Joyerias
        public async Task<IActionResult> Index(String search, int page = 0)
        {
            int totalRecords = 0;
            if (search == null)
            {
                search = "";
            }
            //Obtener los registros totales 
            totalRecords = await _context.Joyerias.CountAsync(
                    d => d.NombreJoya.Contains(search)
                );

            //Obtener datos
            var joyerias = await _context.Joyerias
                .Where(d => d.NombreJoya.Contains(search)).ToListAsync();

            var departamentsResult = joyerias.OrderBy(x => x.NombreJoya)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);
            //Obtenerel total de paginas
            var totalPage = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);

            //Iniciar la clase de paginacion
            PaginationJoyeria = new Pagination<Joyeria>()
            {
                RecordsPerPage = this.RecordsPerPage,
                TotalRecords = totalRecords,
                TotalPage = totalPage,
                CurrentPage = page,
                Search = search,
                Result = departamentsResult
            };

            return View(PaginationJoyeria);

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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName");
            return View();
        }

        // POST: Joyerias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJoyeria,CodigoJoya,NombreJoya,DetalleJoya,Stock,PrecioJoya,IdSend,Img")] Joyeria joyeria)
        {
            
            if (ModelState.IsValid)
            {
                //Guardar Imagen en wwwroot
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(joyeria.Img.FileName);
                string extension = Path.GetExtension(joyeria.Img.FileName);
                joyeria.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await joyeria.Img.CopyToAsync(fileStream);
                }
                //insert record
                _context.Add(joyeria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName", joyeria.IdSend);
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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName", joyeria.IdSend);
            return View(joyeria);
        }

        // POST: Joyerias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJoyeria,CodigoJoya,NombreJoya,DetalleJoya,Stock,PrecioJoya,IdSend,Img")] Joyeria joyeria)
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
            ViewData["IdSend"] = new SelectList(_context.TypeSends, "IdSend", "TypeSendName", joyeria.IdSend);
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

            //delete imagen from wwwroot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", joyeria.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            //Delete the record

            _context.Joyerias.Remove(joyeria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JoyeriaExists(int id)
        {
            return _context.Joyerias.Any(e => e.IdJoyeria == id);
        }


        /////////////////////////////////////////////////////////////////////
        
        
        ////////////////////////////////////////////////////////////////////
    }
}
