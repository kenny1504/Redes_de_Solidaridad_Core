using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class DocenteController : Controller
    {
        private readonly CentrosEscolares _context;

        public DocenteController(CentrosEscolares context)
        {
            _context = context;
        }
        public async Task<IActionResult> Datos_DocenteU(uint? id)
        {

            var data = (from item in _context.Usuarios.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        where item.Id == id && item.Cedula == item3.Cedula
                        select new
                        {
                            cedula = item3.Cedula,
                            nombre = item3.Nombre,
                            apellido1 = item3.Apellido1,
                            apellido2 = item3.Apellido2,
                            sexo = item3.Sexo,
                            direccion = item3.Direccion,
                            correo = item3.Correo,
                            telefono = item3.Telefono,
                            fecha = item3.FechaNacimiento,
                            estado = item4.Estado
                        }).ToList();

            return Json(data);

        }
        public async Task<IActionResult> Mostrar(uint? id)
        {

            var data = (from item2 in _context.Institucion.ToList()
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        where item2.Id == id
                        select new
                        {
                            id = item4.Id,
                            cedula = item3.Cedula,
                            nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                            sexo = item3.Sexo,
                            correo = item3.Correo,
                            telefono = item3.Telefono,
                            estado = item4.Estado
                        });

            return Json(data);

        }
        public async Task<IActionResult> MostrarADMIN(uint? id)
        {

            var data = (from item2 in _context.Institucion.ToList()
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        select new
                        {
                            id = item4.Id,
                            cedula = item3.Cedula,
                            nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                            sexo = item3.Sexo,
                            correo = item3.Correo,
                            telefono = item3.Telefono,
                            estado = item4.Estado
                        });

            return Json(data);

        }


        [Route("Docentes")]
        public IActionResult Index() //Envia vista de Mostrar usuarios
        {

            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";

            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }
            if (usuario != null && tipo == "1") //usuario tipo ADMINISTRADOR
            {
                return View("~/Areas/Docente/Views/Mostrar_Admin.cshtml");
            }
            else if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Docente/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }
    }
}
