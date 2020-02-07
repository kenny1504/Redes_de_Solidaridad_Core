using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class OficiosController : Controller
    {
        private readonly RedesDeSolidaridadContext _context;

        public OficiosController(RedesDeSolidaridadContext context)
        {
            _context = context;
        }

        // GET: Oficios
        public async Task<IActionResult> Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null && usuario[6].ToString() == "True") //verifica si existe una sesion Valida
            {
                return View("~/Areas/Estudiante/Views/Mostrar.cshtml");
            }
            else
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

        }

        // GET: Oficios/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficios = await _context.Oficios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oficios == null)
            {
                return NotFound();
            }

            return View(oficios);
        }

        // GET: Oficios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oficios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Oficios oficios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oficios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oficios);
        }

        // GET: Oficios/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficios = await _context.Oficios.FindAsync(id);
            if (oficios == null)
            {
                return NotFound();
            }
            return View(oficios);
        }

        // POST: Oficios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Nombre")] Oficios oficios)
        {
            if (id != oficios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oficios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OficiosExists(oficios.Id))
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
            return View(oficios);
        }

        // GET: Oficios/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oficios = await _context.Oficios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oficios == null)
            {
                return NotFound();
            }

            return View(oficios);
        }

        // POST: Oficios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var oficios = await _context.Oficios.FindAsync(id);
            _context.Oficios.Remove(oficios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OficiosExists(uint id)
        {
            return _context.Oficios.Any(e => e.Id == id);
        }
    }
}
