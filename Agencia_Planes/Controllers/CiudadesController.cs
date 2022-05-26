using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencia_Planes.Models;

namespace Agencia_Planes.Controllers
{
    public class CiudadesController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public CiudadesController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
              return _context.Ciudades != null ? 
                          View(await _context.Ciudades.ToListAsync()) :
                          Problem("Entity set 'AgenciaViajesContext.Ciudades'  is null.");
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Ciudades == null)
            {
                return NotFound();
            }

            var ciudade = await _context.Ciudades
                .FirstOrDefaultAsync(m => m.CodigoCiudad == id);
            if (ciudade == null)
            {
                return NotFound();
            }

            return View(ciudade);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ciudades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCiudad,NombreCiudad,Geografia,Cultura,Clima,Geolocalizacion")] Ciudade ciudade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciudade);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Ciudades == null)
            {
                return NotFound();
            }

            var ciudade = await _context.Ciudades.FindAsync(id);
            if (ciudade == null)
            {
                return NotFound();
            }
            return View(ciudade);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoCiudad,NombreCiudad,Geografia,Cultura,Clima,Geolocalizacion")] Ciudade ciudade)
        {
            if (id != ciudade.CodigoCiudad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadeExists(ciudade.CodigoCiudad))
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
            return View(ciudade);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Ciudades == null)
            {
                return NotFound();
            }

            var ciudade = await _context.Ciudades
                .FirstOrDefaultAsync(m => m.CodigoCiudad == id);
            if (ciudade == null)
            {
                return NotFound();
            }

            return View(ciudade);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Ciudades == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.Ciudades'  is null.");
            }
            var ciudade = await _context.Ciudades.FindAsync(id);
            if (ciudade != null)
            {
                _context.Ciudades.Remove(ciudade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadeExists(string id)
        {
          return (_context.Ciudades?.Any(e => e.CodigoCiudad == id)).GetValueOrDefault();
        }
    }
}
