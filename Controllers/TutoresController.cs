﻿using System;
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
    public class TutoresController : Controller
    {
        private readonly CentrosEscolares _context;

        public TutoresController(CentrosEscolares context)
        {
            _context = context;
        }

        // GET: Tutores
        public async Task<IActionResult> Index()
        {
            var redesDeSolidaridadContext = _context.Tutores.Include(t => t.OficiosId).Include(t => t.PersonasId);
            return View(await redesDeSolidaridadContext.ToListAsync());
        }

        // GET: Tutores/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutores = await _context.Tutores
                .Include(t => t.OficiosId)
                .Include(t => t.PersonasId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutores == null)
            {
                return NotFound();
            }

            return View(tutores);
        }

        // GET: Tutores/Create
        public IActionResult Create()
        {
            ViewData["Oficiosid"] = new SelectList(_context.Oficios, "Id", "Nombre");
            ViewData["Personasid"] = new SelectList(_context.Personas, "Id", "Apellido1");
            return View();
        }

        // POST: Tutores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Personasid,Oficiosid")] Tutores tutores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Oficiosid"] = new SelectList(_context.Oficios, "Id", "Nombre", tutores.OficiosId);
            ViewData["Personasid"] = new SelectList(_context.Personas, "Id", "Apellido1", tutores.PersonasId);
            return View(tutores);
        }

        // GET: Tutores/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutores = await _context.Tutores.FindAsync(id);
            if (tutores == null)
            {
                return NotFound();
            }
            ViewData["Oficiosid"] = new SelectList(_context.Oficios, "Id", "Nombre", tutores.OficiosId);
            ViewData["Personasid"] = new SelectList(_context.Personas, "Id", "Apellido1", tutores.PersonasId);
            return View(tutores);
        }

        // POST: Tutores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Personasid,Oficiosid")] Tutores tutores)
        {
            if (id != tutores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!TutoresExists(tutores.Id))
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
            ViewData["Oficiosid"] = new SelectList(_context.Oficios, "Id", "Nombre", tutores.OficiosId);
            ViewData["Personasid"] = new SelectList(_context.Personas, "Id", "Apellido1", tutores.PersonasId);
            return View(tutores);
        }

        // GET: Tutores/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutores = await _context.Tutores
                .Include(t => t.OficiosId)
                .Include(t => t.PersonasId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutores == null)
            {
                return NotFound();
            }

            return View(tutores);
        }

        // POST: Tutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var tutores = await _context.Tutores.FindAsync(id);
            _context.Tutores.Remove(tutores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutoresExists(uint id)
        {
            return _context.Tutores.Any(e => e.Id == id);
        }

        public async Task<ActionResult> Ver (int id)// metodo ajax para recuperar datos de estudiantes
        {
            var data =  (from item in  _context.Tutores
                        join item2 in _context.Personas on item.PersonasId equals item2.Id
                        join item3 in _context.Oficios  on item.OficiosId equals item3.Id
                        where item.Id==id
                        select new
                        {
                            Cedula = item2.Cedula,
                            Nombre = item2.Nombre,
                            Apellido1 = item2.Apellido1,
                            Apellido2 = item2.Apellido2,
                            Sexo = item2.Sexo,
                            Fecha = item2.FechaNacimiento.Date,
                            Oficio = item3.Nombre,
                            Correo = item2.Correo,
                            telefono = item2.Telefono,
                            Direccion=item2.Direccion

                        });

            return Json(data);
        }

        public async Task<ActionResult> Datos()// metodo ajax para recuperar datos de estudiantes
        {
            var data = (from item in _context.Tutores.ToList()
                        join item2 in _context.Personas.ToList() on item.PersonasId equals item2.Id
                        join item3 in _context.Oficios.ToList() on item.OficiosId equals item3.Id
                        select new
                        {
                            Idtutor = item.Id,
                            Cedulat = item2.Cedula,
                            Nombret = item2.Nombre+" "+ item2.Apellido1+" "+ item2.Apellido2,
                            Sexot = item2.Sexo,
                            Oficiot = item3.Nombre,
                            Correot = item2.Correo,
                            telefonot = item2.Telefono,
                        });

            return Json(data);
        }
    }
}
