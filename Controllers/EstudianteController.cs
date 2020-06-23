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

        [Route("Estudiantes")]
        public ActionResult Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";

            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "1") //usuario tipo ADMINISTRADOR
            {
                return View("~/Areas/Estudiante/Views/ADMIN/Mostrar.cshtml");
            }
            else if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Estudiante/Views/Institucion/Mostrar.cshtml");
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

        public async Task<ActionResult> Datos()// metodo ajax para recuperar datos de estudiantes (ADMINISTRADOR)
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

        public async Task<ActionResult> Datos2(int id)// metodo ajax para recuperar datos de estudiantes (Institucion)
        {
            var data = (from item in db.Estudiantes.ToList()
                        join item2 in db.Personas.ToList() on item.PersonasId equals item2.Id
                        join item3 in db.Tutores.ToList() on item.TutorId equals item3.Id
                        join item4 in db.Personas.ToList() on item3.PersonasId equals item4.Id
                        where item2.IdInstitucion==id
                        select new
                        {
                            IdEstudiante = item.Id,
                            IdPersona = item2.Id,
                            Codigo = item.CodigoEstudiante,
                            Nombre = item2.Nombre + " " + item2.Apellido1 + " " + item2.Apellido2,
                            sexo = item2.Sexo,
                            direccion = item2.Direccion,
                            tutor = item4.Nombre + " " + item4.Apellido1 + " " + item4.Apellido2,
                            telefono_tutor = item4.Telefono

                        });

            return Json(data);
        }
    }
}