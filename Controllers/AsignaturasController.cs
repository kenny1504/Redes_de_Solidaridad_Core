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

        private readonly CentrosEscolares _context;

        public AsignaturasController(CentrosEscolares context)
        {
            _context = context;
        }


        // GET: Asignaturas
        [Route("Asignaturas")]
        public async Task<IActionResult> Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";
            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Asignatura/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

        }


        // POST: Asignaturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Crear(string Nombre, int idinstitucion)//Metodo para crear una asignatura
        {

            var data = (from item in _context.Detalleasignaturasinstitucion
                        join item2 in _context.Asignaturas on item.IdAsignatura equals item2.Id
                        where item.IdInstitucion == idinstitucion && item2.Nombre==Nombre
                        select new
                        {
                            id = item2.Id,
                            nombre = item2.Nombre

                        }).Count();

            if (data == 0)
            {     //agrega asignaturas


                Asignaturas asignaturas = new Asignaturas();
                asignaturas.Nombre = Nombre;
                _context.Add(asignaturas);
                await _context.SaveChangesAsync();


                Detalleasignaturasinstitucion detalleasignaturasinstitucion = new Detalleasignaturasinstitucion();
                detalleasignaturasinstitucion.IdAsignatura = asignaturas.Id;
                detalleasignaturasinstitucion.IdInstitucion = idinstitucion;
                _context.Add(detalleasignaturasinstitucion);
                await _context.SaveChangesAsync();


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
            if (item2 == null)
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

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)

        {   //Consulta para verificar si existe la materia asignada a algun grado
            var datos = _context.Gradoasignaturas.Where(x => x.AsignaturasId == id).FirstOrDefault();


            if (datos != null) //si existe
            {
                return Json(-1);
            }
            else
            {
                var asignaturas = await _context.Asignaturas.FindAsync(id); //busca la asignatura
                _context.Asignaturas.Remove(asignaturas); //Elimina
                await _context.SaveChangesAsync(); //Guarda
                return Json(1);

            }
        }

        public async Task<ActionResult> Datos(int idinstitucion)// metodo ajax para recuperar datos de asignaturas
        {
            var data = (from item in _context.Detalleasignaturasinstitucion.ToList()
                        join item2 in _context.Asignaturas.ToList() on item.IdAsignatura equals item2.Id
                        where item.IdInstitucion==idinstitucion
                        select new
                        {
                            id=item2.Id,
                            nombre=item2.Nombre

                        }).ToList();

            return Json(data); 
        }

        public async Task<IActionResult> Asignar(int GradoAcademicoId, int AsignaturasId) //metodo para actualizar una asignatura
        {
            var verifica = _context.Gradoasignaturas.Where(x => x.AsignaturasId == AsignaturasId && x.GradoAcademicoId == GradoAcademicoId).FirstOrDefault();

            if (verifica == null)
            {
                Gradoasignaturas gradoasignaturas = new Gradoasignaturas();
                gradoasignaturas.AsignaturasId = AsignaturasId;
                gradoasignaturas.GradoAcademicoId = GradoAcademicoId;

                _context.Add(gradoasignaturas);
                await _context.SaveChangesAsync(); //guarda
                return Json(1);
            }
            else
                return Json(-1);
        }


        [HttpPost]
        public ActionResult MateriasGrado(int idGrado, int idinstitucion) //metodo para cargar materias segun la oferta
        {
            var data = (from item in _context.Detalleasignaturasinstitucion.ToList()
                        join item2 in _context.Asignaturas.ToList() on item.IdAsignatura equals item2.Id
                        join item3 in _context.Gradoasignaturas.ToList() on item2.Id equals item3.AsignaturasId
                        join item4 in _context.Gradoacademico.ToList() on item3.GradoAcademicoId equals item4.Id
                        where item4.Id == idGrado && item.IdInstitucion == idinstitucion
                        select new
                        {
                            Id = item3.Id,
                            Nombre = item2.Nombre
                        }
                ).ToList();

            return Json(data);

        }

    }
}
