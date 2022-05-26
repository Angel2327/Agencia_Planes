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
    public class ComprasController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public ComprasController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var agenciaViajesContext = _context.Compras.Include(c => c.Cedula_Compra).Include(c => c.CodigoDeVuelo_Compra).Include(c => c.CodigoPlan_Compra);
            return View(await agenciaViajesContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Cedula_Compra)
                .Include(c => c.CodigoDeVuelo_Compra)
                .Include(c => c.CodigoPlan_Compra)
                .FirstOrDefaultAsync(m => m.FechaCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.Personas, "Cedula", "Cedula");
            ViewData["CodigoDeVuelo"] = new SelectList(_context.TransporteEnAvion, "CodigoDeVuelo", "CodigoDeVuelo");
            ViewData["CodigoPlan"] = new SelectList(_context.PlanViajes, "CodigoPlan", "CodigoPlan");
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaCompra,Cedula,CodigoPlan,CodigoDeVuelo,PrecioPagado,NumeroPersonas")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.Personas, "Cedula", "Cedula", compra.Cedula);
            ViewData["CodigoDeVuelo"] = new SelectList(_context.TransporteEnAvion, "CodigoDeVuelo", "CodigoDeVuelo", compra.CodigoDeVuelo);
            ViewData["CodigoPlan"] = new SelectList(_context.PlanViajes, "CodigoPlan", "CodigoPlan", compra.CodigoPlan);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.Personas, "Cedula", "Cedula", compra.Cedula);
            ViewData["CodigoDeVuelo"] = new SelectList(_context.TransporteEnAvion, "CodigoDeVuelo", "CodigoDeVuelo", compra.CodigoDeVuelo);
            ViewData["CodigoPlan"] = new SelectList(_context.PlanViajes, "CodigoPlan", "CodigoPlan", compra.CodigoPlan);
            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("FechaCompra,Cedula,CodigoPlan,CodigoDeVuelo,PrecioPagado,NumeroPersonas")] Compra compra)
        {
            if (id != compra.FechaCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.FechaCompra))
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
            ViewData["Cedula"] = new SelectList(_context.Personas, "Cedula", "Cedula", compra.Cedula);
            ViewData["CodigoDeVuelo"] = new SelectList(_context.TransporteEnAvion, "CodigoDeVuelo", "CodigoDeVuelo", compra.CodigoDeVuelo);
            ViewData["CodigoPlan"] = new SelectList(_context.PlanViajes, "CodigoPlan", "CodigoPlan", compra.CodigoPlan);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Cedula_Compra)
                .Include(c => c.CodigoDeVuelo_Compra)
                .Include(c => c.CodigoPlan_Compra)
                .FirstOrDefaultAsync(m => m.FechaCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(DateTime id)
        {
          return (_context.Compras?.Any(e => e.FechaCompra == id)).GetValueOrDefault();
        }
    }
}
