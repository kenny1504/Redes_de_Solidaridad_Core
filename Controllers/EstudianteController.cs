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
        private readonly RedesDeSolidaridadContext db = new RedesDeSolidaridadContext();
        
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

        // GET: Estudiante/Details/5s
        public ActionResult Details(int id)
        {
            return View();
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

        public async Task<ActionResult> Datos()
        {
            var data = (from item in db.Estudiantes.ToList()
                    join item2 in db.Personas.ToList() on item.Personasid equals item2.Id
                    select new {
                        IdEstudiante = item.Id,
                        IdPersona = item2.Id,
                        Codigo = item.CodigoEstudiante,
                        Nombre = item2.Nombre + " " + item2.Apellido1 + " " + item2.Apellido2
                    });

            return Json(data);
        }
    }
}