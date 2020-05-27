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
        public IActionResult Index() //Envia vista de Mostrar usuarios
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null) //verifica si existe una sesion Valida
            {
                return View("~/Areas/Usuarios/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }

        //metodo para Cargar todas las Docentes
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
        //metodo para Cargar todas las instituciones
        public async Task<ActionResult> Usarios_Instituciones()// metodo ajax para recuperar datos de Usuarios instituciones
        {
            var data = (from item in _context.UsuariosInstituciones.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        select new
                        {
                            IdUsuario = item.IdInstitucion,
                            Usuario = item.Usuario,
                            Contraseña = item.Contraseña,
                            Nombre = item2.Nombre,
                            Direccion = item2.Direccion,
                        });

            return Json(data);
        }

        [HttpPost] //metodo para Agregar una institucion
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

                return Json(institucion.Id);
            }
            return Json(-1);
        }
        //metodo para Eliminar una institucion
        public async Task<IActionResult> Eliminar_Institucion(uint? id)
        {     //Consulta JOIN para verificar si existe personas agregada a esta institucion
            var instituc = _context.Institucion.Join(_context.Personas, i => i.Id, p => p.IdInstitucion, (i, p) => i).Where(x => x.Id == id).FirstOrDefault();


            if (instituc != null) //si existe
            {
                return Json(-1);
            }
            else
            {
                var usuario = _context.UsuariosInstituciones.Where(u => u.IdInstitucion == id).FirstOrDefault(); //busca la asignatura
                _context.UsuariosInstituciones.Remove(usuario); //Elimina
                await _context.SaveChangesAsync(); //Guarda

                var institucion = _context.Institucion.Where(i => i.Id == id).FirstOrDefault();
                _context.Institucion.Remove(institucion); //Elimina
                await _context.SaveChangesAsync(); //Guarda

                return Json(1);

            }
        }
        //metodo para actualizar un usuario Institucion
        public async Task<IActionResult> Editar_Institucion([Bind("Id,Institucion,Direcccion,Usuario,Contraseña")] UsuarioInstitucion usuar) //metodo para actualizar una asignatura
        {
            var inst = _context.Institucion.Where(x => x.Nombre == usuar.Institucion).FirstOrDefault(); //verifica si existe una institucion 
            var user = _context.UsuariosInstituciones.Where(x => x.Usuario == usuar.Usuario).FirstOrDefault(); //verifica si existe una institucion
                   
            if (inst != null && user != null) // si existen nombre de institucion y nombre de usuario, verifica que sea el mismo a actualizar
            {
             
                if(inst.Id==usuar.Id && user.IdInstitucion== usuar.Id)// si es el mismo a actualizar
                {
                    user.Usuario = usuar.Usuario; //agrega
                    user.Contraseña = usuar.Contraseña; //agrega 
                    _context.Update(user);
                    await _context.SaveChangesAsync(); //guarda

                    inst.Nombre = usuar.Institucion; //agrega
                    inst.Direccion = usuar.Direcccion; //agrega 
                    _context.Update(inst);
                    await _context.SaveChangesAsync(); //guarda

                    return Json(1);

                }
                else // de lo contrario retorna -1
                    return Json(-1);

            }
            else // si no existen nombre de institucion y nombre de usuario Actualiza
            {
                var inst2 = _context.Institucion.Where(x => x.Id == usuar.Id).FirstOrDefault(); //busca
                var user2 = _context.UsuariosInstituciones.Where(x => x.IdInstitucion == usuar.Id).FirstOrDefault(); //busca

                user2.Usuario = usuar.Usuario; //agrega
                user2.Contraseña = usuar.Contraseña; //agrega 
                _context.Update(user2);
                await _context.SaveChangesAsync(); //guarda

                inst2.Nombre = usuar.Institucion; //agrega
                inst2.Direccion = usuar.Direcccion; //agrega 
                _context.Update(inst2);
                await _context.SaveChangesAsync(); //guarda

                return Json(1);
            }
        }

        public async Task<IActionResult> Institucion(uint? id) //metodo cargar datos en modal
        {
            var data = (from item in _context.UsuariosInstituciones
                        join item2 in _context.Institucion on item.IdInstitucion equals item2.Id
                        where item.IdInstitucion==id
                        select new
                        {
                            IdUsuario = item.IdInstitucion,
                            Usuario = item.Usuario,
                            Contraseña = item.Contraseña,
                            Nombre = item2.Nombre,
                            Direccion = item2.Direccion,
                        });

            return Json(data);
        }
    }
}
