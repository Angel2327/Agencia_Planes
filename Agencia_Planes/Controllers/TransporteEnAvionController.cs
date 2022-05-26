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
    public class TransporteEnAvionController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public TransporteEnAvionController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: TransporteEnAvions
        public async Task<IActionResult> Index()
        {
              return _context.TransporteEnAvion != null ? 
                          View(await _context.TransporteEnAvion.ToListAsync()) :
                          Problem("Entity set 'AgenciaViajesContext.TransporteEnAvions'  is null.");
        }

        // GET: TransporteEnAvions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TransporteEnAvion == null)
            {
                return NotFound();
            }

            var transporteEnAvion = await _context.TransporteEnAvion
                .FirstOrDefaultAsync(m => m.CodigoDeVuelo == id);
            if (transporteEnAvion == null)
            {
                return NotFound();
            }

            return View(transporteEnAvion);
        }

        // GET: TransporteEnAvions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransporteEnAvions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoDeVuelo,Abordo,Aeroliena,HoraSalida,HoraLlegada")] TransporteEnAvion transporteEnAvion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transporteEnAvion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transporteEnAvion);
        }

        // GET: TransporteEnAvions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TransporteEnAvion == null)
            {
                return NotFound();
            }

            var transporteEnAvion = await _context.TransporteEnAvion.FindAsync(id);
            if (transporteEnAvion == null)
            {
                return NotFound();
            }
            return View(transporteEnAvion);
        }

        // POST: TransporteEnAvions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoDeVuelo,Abordo,Aeroliena,HoraSalida,HoraLlegada")] TransporteEnAvion transporteEnAvion)
        {
            if (id != transporteEnAvion.CodigoDeVuelo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transporteEnAvion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransporteEnAvionExists(transporteEnAvion.CodigoDeVuelo))
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
            return View(transporteEnAvion);
        }

        // GET: TransporteEnAvions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TransporteEnAvion == null)
            {
                return NotFound();
            }

            var transporteEnAvion = await _context.TransporteEnAvion
                .FirstOrDefaultAsync(m => m.CodigoDeVuelo == id);
            if (transporteEnAvion == null)
            {
                return NotFound();
            }

            return View(transporteEnAvion);
        }

        // POST: TransporteEnAvions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TransporteEnAvion == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.TransporteEnAvions'  is null.");
            }
            var transporteEnAvion = await _context.TransporteEnAvion.FindAsync(id);
            if (transporteEnAvion != null)
            {
                _context.TransporteEnAvion.Remove(transporteEnAvion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransporteEnAvionExists(string id)
        {
          return (_context.TransporteEnAvion?.Any(e => e.CodigoDeVuelo == id)).GetValueOrDefault();
        }
    }
}
