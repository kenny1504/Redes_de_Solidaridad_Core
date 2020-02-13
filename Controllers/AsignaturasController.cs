﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;
using System.Data;

namespace Redes_De_Solidaridad.Controllers
{
    public class AsignaturasController : Controller
    {
        private readonly RedesDeSolidaridadContext _context;

        public AsignaturasController(RedesDeSolidaridadContext context)
        {
            _context = context;
        }


        // GET: Asignaturas
        public async Task<IActionResult> Index()
        {
           
            var data = await _context.Asignaturas.ToListAsync();
            // var data = await _context.Asignaturas.Include(x => x.Detallematriculas).ToListAsync();
            /* ViewBag.pepa = (from i in _context.Personas
                             join x in _context.Docentes on i.Id equals x.Id
                             join y in _context.Ofertas on x.Id equals y.Docenteid
                             select new OfertaView{ NombreDocente = i.Nombre, DescripcionOferta = y.Descripcion }).ToList(); */
            //Linq ejemplo

            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null && usuario[6].ToString() == "True") //verifica si existe una sesion Valida
            {
                return View("~/Areas/Asignaturas/Views/Mostrar.cshtml", data);
            }
            else //si no existe una sesion retorna inicio de sesion
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

           
        }
        
        // GET: Asignaturas/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturas = await _context.Asignaturas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturas == null)
            {
                return NotFound();
            }

            return View(asignaturas);
        }

        // GET: Asignaturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Asignaturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Crear(string Nombre)//Metodo para crear una asignatura
        {
                Asignaturas asignaturas = new Asignaturas();
                asignaturas.Nombre = Nombre;
                _context.Add(asignaturas);
                var num = await _context.SaveChangesAsync();
            var id = asignaturas.Id;
            if (num == -1)
            {
                var error = new[]
               {
                    new {

                            Nombre = "La materia la existe",
                            tipo=-1
                       }
                };

                return Json(error);

            }
            else
            {
                var materia = new[]
                {
                    new {

                            Nombre = Nombre,
                            id = id,
                            tipo=1
                       }
                };

                return Json(materia);
            }
        }

        // GET: Asignaturas/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturas = await _context.Asignaturas.FindAsync(id);
            if (asignaturas == null)
            {
                return NotFound();
            }
            return View("~/Areas/Asignaturas/Views/Editar.cshtml", asignaturas);
        }

        // POST: Asignaturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Nombre")] Asignaturas asignaturas)
        {
            if (id != asignaturas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturasExists(asignaturas.Id))
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
            return View("~/Areas/Asignaturas/Views/Editar.cshtml", asignaturas);
        }

        // GET: Asignaturas/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturas = await _context.Asignaturas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturas == null)
            {
                return NotFound();
            }

            return View(asignaturas);
        }

        // POST: Asignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var asignaturas = await _context.Asignaturas.FindAsync(id);
            _context.Asignaturas.Remove(asignaturas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturasExists(uint id)
        {
            return _context.Asignaturas.Any(e => e.Id == id);
        }
    }
}
