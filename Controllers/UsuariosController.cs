﻿using System;
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
        private readonly CentrosEscolares _context;

        public UsuariosController(CentrosEscolares context)
        {
            _context = context;
        }

        [Route("Usuarios")]
        public IActionResult Index() //Envia vista de Mostrar usuarios
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";
            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona a string
            }



            if (usuario != null && tipo == "1") //usuario tipo ADMINISTRADOR
            {
                return View("~/Areas/Usuarios/Views/Admin/Mostrar.cshtml");
            }
            else if (usuario != null && tipo == "3")//usuario tipo DOCENTE
            {
                return View("~/Areas/Usuarios/Views/Docente/Mostrar.cshtml");
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

        public async Task<IActionResult> Institucion(uint? id) //metodo cargar datos en modal usuario institucion
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

        public async Task<IActionResult> Docente(uint? id) //metodo cargar datos en modal usuario docente
        {
            var data = (from item in _context.Usuarios.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        where item.Cedula == item3.Cedula && item.Id == id
                        select new
                        {
                            IdUsuario = item.Id,
                            cedula=item.Cedula,
                            Usuario = item.Usuario,
                            Contrasena = item.Contraseña,
                            Institucion = item2.Id,
                        });

            return Json(data);
        }

        //recupera Todas las instituciones
        public async Task<IActionResult> Instituciones() //metodo cargar datos en modal
        {
            var data = await _context.Institucion.ToListAsync();

            return Json(data);
        }


        [HttpPost] //metodo para Agregar un usuario docente
        public async Task<ActionResult> Registro_Usuario_Docente([Bind("Cedula,Usuario,Contraseña,Institucion")] usuarioDocenteview docent)
        {
            var data = (from item in _context.Personas //verifica si la cedula ingresada pertenece a un docente de ka institucion
                        join item2 in _context.Docentes on item.Id equals item2.PersonasId
                        join item3 in _context.Institucion on item.IdInstitucion equals item3.Id
                        where item.Cedula==docent.Cedula && item.IdInstitucion==docent.Institucion
                        select new
                        {
                            Nombre = item.Nombre + " "+item.Apellido1+item.Apellido2,
                            intitucion= item3.Nombre
                        
                        });

            if(data.Count()>0)
            {
                var user = _context.Usuarios.Where(x => x.Usuario == docent.Usuario && x.IdInstitucion==docent.Institucion).FirstOrDefault(); //verifica si el nombre de usuario existe en la institucion
               
                if (user == null)
                {     //agrega asignaturas
                    Usuarios usuarios = new Usuarios();

                    usuarios.Cedula = docent.Cedula;
                    usuarios.Usuario = docent.Usuario;
                    usuarios.Contraseña = docent.Contraseña;
                    usuarios.IdInstitucion = docent.Institucion;
                    _context.Add(usuarios);
                    await _context.SaveChangesAsync();

                    var r = from item2 in data
                            select new
                            {
                                id = usuarios.Id,
                                Nombbre = item2.Nombre,
                                user = usuarios.Usuario,
                                Pass = usuarios.Contraseña,
                                Inst = item2.intitucion
                            };


                    return Json(r);
                }
                else
                    return Json(-1);

            }
            else
                return Json(0);
        }

        public async Task<IActionResult> Eliminar_Usuario_Docente(uint? id)
        {    
           
                var Usuario_D = _context.Usuarios.Where(u => u.Id == id).FirstOrDefault();
                _context.Usuarios.Remove(Usuario_D); //Elimina
                await _context.SaveChangesAsync(); //Guarda

                return Json(1);
        }

        [HttpPost] //metodo para Agregar un usuario docente
        public async Task<ActionResult> Editar_Usuario_Docente([Bind("Id,Cedula,Usuario,Contraseña,Institucion")] usuarioDocenteview docent)
        {
            var data = (from item in _context.Personas //verifica si la cedula ingresada pertenece a un docente de la institucion
                        join item2 in _context.Docentes on item.Id equals item2.PersonasId
                        join item3 in _context.Institucion on item.IdInstitucion equals item3.Id
                        where item.Cedula == docent.Cedula && item.IdInstitucion == docent.Institucion
                        select new
                        {
                            Nombre = item.Nombre + " " + item.Apellido1 + item.Apellido2,
                            intitucion = item3.Nombre

                        });

            if (data.Count() > 0)
            {
               
               
                var users = _context.Usuarios.Where(x => x.Usuario == docent.Usuario && x.IdInstitucion == docent.Institucion).FirstOrDefault(); //verifica si el nombre de usuario existe en la institucion

                if (users == null)
                {
                    var user = _context.Usuarios.Where(x => x.Id==docent.Id).FirstOrDefault();
                    user.Cedula = docent.Cedula;
                    user.Usuario = docent.Usuario;
                    user.Contraseña = docent.Contraseña;
                    user.IdInstitucion = docent.Institucion;
                    _context.Update(user);
                    await _context.SaveChangesAsync(); //guarda

                    var r = from item2 in data
                            select new
                            {
                                id = user.Id,
                                Nombbre = item2.Nombre,
                                user = user.Usuario,
                                Pass = user.Contraseña,
                                Inst = item2.intitucion
                            };
                    return Json(r);
                }
                else
                  if (users.Id == docent.Id)
                  {
                    var user = _context.Usuarios.Where(x => x.Id == docent.Id).FirstOrDefault();
                    user.Cedula = docent.Cedula;
                    user.Usuario = docent.Usuario;
                    user.Contraseña = docent.Contraseña;
                    user.IdInstitucion = docent.Institucion;
                    _context.Update(user);
                    await _context.SaveChangesAsync(); //guarda

                    var r = from item2 in data
                            select new
                            {
                                id = user.Id,
                                Nombbre = item2.Nombre,
                                user = user.Usuario,
                                Pass = user.Contraseña,
                                Inst = item2.intitucion
                            };
                    return Json(r);
                 }
                else 
                    return Json(-1);

            }
            else// error el usuario que ingreso ya existe en esta institucion
                return Json(0);
        }

        public async Task<ActionResult> Usarios_Docentes2(uint? id)// metodo ajax para recuperar datos de Usuarios docentes
        {
            var data = (from item in _context.Usuarios.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        where item.Cedula == item3.Cedula && item2.Id==id
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
    }
}
