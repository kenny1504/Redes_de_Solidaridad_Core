using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Models;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly CentrosEscolares db = new CentrosEscolares();
        
        // GET: Estudiante
        public ActionResult Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null && usuario[6].ToString() == "True") //verifica si existe una sesion Valida
            {
                return View("~/Areas/Estudiante/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

        }

        [HttpPost]
        public ActionResult Detalles(int id) //metodo para cargar datos de estudiantes DETALLES
        {
            var data = (from item in db.Estudiantes
                        join item2 in db.Personas on item.PersonasId equals item2.Id
                        join item3 in db.Tutores on item.TutorId equals item3.Id
                        join item4 in db.Personas on item3.PersonasId equals item4.Id
                        join item5 in db.Parentescos on item.ParentescosId equals item5.Id
                        where item.Id==id
                        select new
                        {
                            Codigo = item.CodigoEstudiante,
                            Nombre = item2.Nombre,
                            Apellido1 = item2.Apellido1,
                            Apellido2 = item2.Apellido2,
                            sexo = item2.Sexo,
                            fecha = item2.FechaNacimiento.Date,
                            direccion = item2.Direccion,
                            tutor = item4.Nombre + " " + item4.Apellido1 + " " + item4.Apellido2,
                            telefono = item4.Telefono,
                            parentesco=item5.Parentesco,
                            idtutor=item3.Id
                        }) ;

            return Json(data);
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudiante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estudiante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estudiante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Datos()// metodo ajax para recuperar datos de estudiantes
        {
            var data = (from item in db.Estudiantes.ToList()
                    join item2 in db.Personas.ToList() on item.PersonasId equals item2.Id
                    join item3 in db.Tutores.ToList() on item.TutorId equals item3.Id
                    join item4 in db.Personas.ToList() on item3.PersonasId equals item4.Id
                    select new {
                        IdEstudiante = item.Id,
                        IdPersona = item2.Id,
                        Codigo = item.CodigoEstudiante,
                        Nombre = item2.Nombre + " " + item2.Apellido1 + " " + item2.Apellido2,
                        sexo= item2.Sexo,
                        direccion = item2.Direccion,
                        tutor =item4.Nombre +" "+item4.Apellido1+" "+item4.Apellido2,
                        telefono_tutor=item4.Telefono

                    });

            return Json(data);
        }
    }
}