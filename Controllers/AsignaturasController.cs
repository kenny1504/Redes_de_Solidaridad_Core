using System;
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

            var asignatura = _context.Asignaturas.Where(x => x.Nombre == Nombre).FirstOrDefault(); //verifica si existe una asignatura
            if (asignatura==null) 
            {     //agrega asignaturas
                Asignaturas asignaturas = new Asignaturas();
                asignaturas.Nombre = Nombre;
                _context.Add(asignaturas);
                var num = await _context.SaveChangesAsync();
                var materia = new[]
               {
                    new {

                            Nombre = Nombre,
                            id = asignaturas.Id,
                            tipo =1
                       }
                };

                return Json(materia);
            }
            else
            {
                var error = new[]
              {
                    new {

                            Nombre = "La asignatura ya existe",
                            tipo=-1
                       }
              };

                return Json(error);
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
        public async Task<IActionResult> Editar(List<String> values) //metodo para actualizar una asignatura
        {

            var item= _context.Asignaturas.Where(x => x.Id == int.Parse(values[0])).FirstOrDefault(); //Busca la asignatura
            var item2 = _context.Asignaturas.Where(x => x.Nombre ==values[1]).FirstOrDefault(); //verifica que No existe el nuevo nombre
            if (item != null && item2==null)
            {
                item.Nombre = values[1]; //agrega nuevo nombre
                _context.Entry(item).State = EntityState.Modified; //modifica
                var num = await _context.SaveChangesAsync(); //guarda
                return Json(1);

            }
            else
            {
                if (item2 != null)
                    return Json(-1);
                else
                    return Json(0);
            }
                      
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
