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
using System.Text.Json;
using System.Text.Json.Serialization;

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
        // POST: Asignaturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Editar(Asignaturas Materia) //metodo para actualizar una asignatura
        {
            var item2 = _context.Asignaturas.Where(x => x.Nombre == Materia.Nombre).FirstOrDefault(); //verifica que No existe el nuevo nombre
            if ( item2 == null)
            {
                Materia.Nombre = Materia.Nombre; //agrega nuevo nombre
                _context.Update(Materia);
                //_context.Entry(Materia).State = EntityState.Modified; //modifica
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
        public async Task<IActionResult> Eliminar(uint? id)

        {     //Consulta JOIN para verificar si existe la materia asignada a algun grado
            var datos = _context.Asignaturas.Join(_context.Gradoaasignaturas, a => a.Id, gr => gr.Asignaturaid, (a, gr) => a).Where(x => x.Id == id).FirstOrDefault();


            if (datos != null) //si existe
            {
                return Json(-1);
            }
            else
            {
                var asignaturas = await _context.Asignaturas.FindAsync(id);
                _context.Asignaturas.Remove(asignaturas);
                await _context.SaveChangesAsync();
                return Json(1);

            }
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
