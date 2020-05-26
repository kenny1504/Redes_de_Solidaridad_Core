using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad
{
    public class UsuariosController : Controller
    {
        private readonly RedesDeSolidaridadContext _context;

        public UsuariosController(RedesDeSolidaridadContext context)
        {
            _context = context;
        }

        [Route("Usuarios")]
        public IActionResult Index() //Envia vista de inicio de sesion
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null) //verifica si existe una sesion Valida
            {
                return View("~/Areas/Usuarios/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }

        public async Task<ActionResult> Usarios_Docentes()// metodo ajax para recuperar datos de Usuarios docentes
        {
            var data = (from item in _context.Usuarios.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        where item.Cedula == item3.Cedula
                        select new
                        {
                            IdUsuario = item.Id,
                            Usuario = item.Usuario,
                            Contraseña = item.Contraseña,
                            Nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                            Institucion = item2.Nombre,
                        });

            return Json(data);
        }

        public async Task<ActionResult> Usarios_Instituciones()// metodo ajax para recuperar datos de Usuarios instituciones
        {
            var data = (from item in _context.UsuariosInstituciones.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        select new
                        {
                            IdUsuario = item.Id,
                            Usuario = item.Usuario,
                            Contraseña = item.Contraseña,
                            Nombre = item2.Nombre,
                            Direccion = item2.Direccion,
                        });

            return Json(data);
        }
        [HttpPost]
        public async Task<ActionResult> Registro([Bind("Institucion,Direcccion,Usuario,Contraseña")] UsuarioInstitucion usuar)
        {
            var inst = _context.Institucion.Where(x => x.Nombre == usuar.Institucion).FirstOrDefault(); //verifica si existe una institucion 
            var user = _context.UsuariosInstituciones.Where(x => x.Usuario== usuar.Usuario).FirstOrDefault(); //verifica si existe una institucion

            if (inst == null && user== null)
            {     //agrega asignaturas
                Institucion institucion = new Institucion();
                institucion.Nombre = usuar.Institucion;
                institucion.Direccion = usuar.Direcccion;
                _context.Add(institucion);
                await _context.SaveChangesAsync();

               Usuariosinstituciones usarios_Institucion = new Usuariosinstituciones();
                usarios_Institucion.Usuario = usuar.Usuario;
                usarios_Institucion.Contraseña = usuar.Contraseña;
                usarios_Institucion.IdInstitucion = institucion.Id;
                _context.Add(usarios_Institucion);
                await _context.SaveChangesAsync();

                return Json(1);
            }
            return Json(-1);
        }
    }
}
