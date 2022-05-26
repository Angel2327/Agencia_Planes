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
    public class PlanViajesController : Controller
    {
        private readonly AgenciaViajesContext _context;

        public PlanViajesController(AgenciaViajesContext context)
        {
            _context = context;
        }

        // GET: PlanViajes
        public async Task<IActionResult> Index()
        {
            var agenciaViajesContext = _context.PlanViajes.Include(p => p.CodigoCiudad_PlanViaje);
            return View(await agenciaViajesContext.ToListAsync());
        }

        // GET: PlanViajes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PlanViajes == null)
            {
                return NotFound();
            }

            var planViaje = await _context.PlanViajes
                .Include(p => p.CodigoCiudad_PlanViaje)
                .FirstOrDefaultAsync(m => m.CodigoPlan == id);
            if (planViaje == null)
            {
                return NotFound();
            }

            return View(planViaje);
        }

        // GET: PlanViajes/Create
        public IActionResult Create()
        {
            ViewData["CodigoCiudad"] = new SelectList(_context.Ciudades, "CodigoCiudad", "CodigoCiudad");
            return View();
        }

        // POST: PlanViajes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoPlan,CodigoCiudad,NombrePlan,ActividadesIncluidas,Costo,IncluyeHospedaje,FechaInicio,FechaFin")] PlanViaje planViaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planViaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCiudad"] = new SelectList(_context.Ciudades, "CodigoCiudad", "CodigoCiudad", planViaje.CodigoCiudad);
            return View(planViaje);
        }

        // GET: PlanViajes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PlanViajes == null)
            {
                return NotFound();
            }

            var planViaje = await _context.PlanViajes.FindAsync(id);
            if (planViaje == null)
            {
                return NotFound();
            }
            ViewData["CodigoCiudad"] = new SelectList(_context.Ciudades, "CodigoCiudad", "CodigoCiudad", planViaje.CodigoCiudad);
            return View(planViaje);
        }

        // POST: PlanViajes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CodigoPlan,CodigoCiudad,NombrePlan,ActividadesIncluidas,Costo,IncluyeHospedaje,FechaInicio,FechaFin")] PlanViaje planViaje)
        {
            if (id != planViaje.CodigoPlan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planViaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanViajeExists(planViaje.CodigoPlan))
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
            ViewData["CodigoCiudad"] = new SelectList(_context.Ciudades, "CodigoCiudad", "CodigoCiudad", planViaje.CodigoCiudad);
            return View(planViaje);
        }

        // GET: PlanViajes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PlanViajes == null)
            {
                return NotFound();
            }

            var planViaje = await _context.PlanViajes
                .Include(p => p.CodigoCiudad_PlanViaje)
                .FirstOrDefaultAsync(m => m.CodigoPlan == id);
            if (planViaje == null)
            {
                return NotFound();
            }

            return View(planViaje);
        }

        // POST: PlanViajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PlanViajes == null)
            {
                return Problem("Entity set 'AgenciaViajesContext.PlanViajes'  is null.");
            }
            var planViaje = await _context.PlanViajes.FindAsync(id);
            if (planViaje != null)
            {
                _context.PlanViajes.Remove(planViaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanViajeExists(long id)
        {
          return (_context.PlanViajes?.Any(e => e.CodigoPlan == id)).GetValueOrDefault();
        }
    }
}
