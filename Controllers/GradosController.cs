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
    public class GradosController : Controller
    {
        private readonly RedesDeSolidaridadContext _context;

        //Aqui estuvo Felix
        public GradosController(RedesDeSolidaridadContext context)
        {
            _context = context;
        }

        // GET: Grados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gradoacademico.ToListAsync());
        }

        // GET: Grados/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grados = await _context.Gradoacademico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grados == null)
            {
                return NotFound();
            }

            return View(grados);
        }

        // GET: Grados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Grado")] Gradoacademico grados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grados);
        }

        // GET: Grados/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grados = await _context.Gradoacademico.FindAsync(id);
            if (grados == null)
            {
                return NotFound();
            }
            return View(grados);
        }

        // POST: Grados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Grado")] Gradoacademico grados)
        {
            if (id != grados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!GradosExists(grados.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grados);
        }

        // GET: Grados/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grados = await _context.Gradoacademico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grados == null)
            {
                return NotFound();
            }

            return View(grados);
        }

        // POST: Grados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var grados = await _context.Gradoacademico.FindAsync(id);
            _context.Gradoacademico.Remove(grados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradosExists(uint id)
        {
            return _context.Gradoacademico.Any(e => e.Id == id);
        }

        public async Task<ActionResult> Datos()// metodo ajax para recuperar 
        {
            var data = await _context.Gradoacademico.ToListAsync();
            return Json(data);
        }
    }
}
