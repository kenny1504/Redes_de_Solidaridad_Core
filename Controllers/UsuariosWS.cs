using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;



namespace Redes_De_Solidaridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UsuariosWS : ControllerBase
    {

        private readonly CentrosEscolares _context;

        public UsuariosWS(CentrosEscolares context)
        {
            _context = context;
        }

        [HttpGet("usuarios/Docentes")]

        public async Task<ActionResult<List<usuariosWS>>> Usuarios_Docentes(Busqueda inst)
        {

            var data = new List<usuariosWS>();

            data = (from item in _context.Usuarios.ToList()
                    join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                    join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                    where item.Cedula == item3.Cedula && item.IdInstitucion == inst.Id
                    select new usuariosWS
                    {
                        NombreDeUsuario = item.Usuario,
                        Nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                        Id = item.Id,
                        tipo = 2,
                        Cedula = item.Cedula,
                        Institucion = item2.Nombre

                    }).ToList();

            return data;
        }


        [HttpGet("usuarios/Docente")] //Metodo para recuperar datos de un usuario docente
        public async Task<ActionResult<List<Personas>>> Datos_Docente(BusquedaUD u)
        {

            var data = new List<Personas>();

            data = (from item in _context.Usuarios.ToList()
                    join item3 in _context.Personas.ToList() on item.Cedula equals item3.Cedula
                    where item.Cedula == u.Cedula
                    select new Personas
                    {
                        Cedula = item3.Cedula,
                        Nombre = item3.Nombre,
                        Apellido1 = item3.Apellido1,
                        Apellido2 = item3.Apellido2,
                        Sexo = item3.Sexo,
                        Direccion = item3.Direccion,
                        Correo = item3.Correo,
                        Telefono = item3.Telefono,
                        FechaNacimiento = item3.FechaNacimiento

                    }).ToList();

            return data;
        }
        [HttpPost("usuarios/AgregarDocente")] //metodo para Agregar un usuario docente
        public async Task<usuariosWS> Registro_Usuario_Docente(Usuarios docent)
        {
            usuariosWS r;
            userview data = (from item in _context.Personas //verifica si la cedula ingresada pertenece a un docente de ka institucion
                             join item2 in _context.Docentes on item.Id equals item2.PersonasId
                             join item3 in _context.Institucion on item.IdInstitucion equals item3.Id
                             where item.Cedula == docent.Cedula && item.IdInstitucion == docent.IdInstitucion
                             select new userview
                             {
                                 username = item.Nombre + " " + item.Apellido1 + item.Apellido2,
                                 password = item3.Nombre

                             }).FirstOrDefault();

            if (data != null)
            {
                var user = _context.Usuarios.Where(x => x.Usuario == docent.Usuario && x.IdInstitucion == docent.IdInstitucion).FirstOrDefault(); //verifica si el nombre de usuario existe en la institucion

                if (user == null)
                {     //agrega asignaturas
                    Usuarios usuarios = new Usuarios();

                    usuarios.Cedula = docent.Cedula;
                    usuarios.Usuario = docent.Usuario;
                    usuarios.Contraseña = docent.Contraseña;
                    usuarios.IdInstitucion = docent.IdInstitucion;
                    _context.Add(usuarios);
                    await _context.SaveChangesAsync();

                    r = new usuariosWS
                    {
                        Id = usuarios.Id,
                        Nombre = data.username,
                        NombreDeUsuario = usuarios.Usuario,
                        tipo = 2,
                        Cedula = usuarios.Cedula,
                        Institucion = data.password
                    };
                    return r;
                }
                else
                {
                    r = new usuariosWS
                    {
                        Id = -1,
                        Nombre = "Ya existe",
                        NombreDeUsuario = "Ya existe",
                        tipo = -1,
                        Cedula = "Ya existe",
                        Institucion = "Ya existe"
                    };
                    return r;// error el usuario que ingreso ya existe en esta institucion
                }


            }
            else
            {
                r = new usuariosWS
                {
                    Id = 0,
                    Nombre = "No pertenece",
                    NombreDeUsuario = "No pertenece",
                    tipo = 0,
                    Cedula = "No pertenece",
                    Institucion = "No pertenece"
                };
                return r; //si retorna 0 es porque el numero de cedula no pertene a un docente de la institucion
            }

        }


        [HttpPut("editarUsuario")]
        public async Task<int> Editar_Usuario(Usuarios usuarios) //metodo para actualizar una asignatura
        {

            var user = _context.Usuarios.Where(x => x.Cedula == usuarios.Cedula).FirstOrDefault(); //verifica si existe una institucion

            //si solo cambia el nombre de usuario
            if (usuarios.Usuario != null  && usuarios.Contraseña == null) // si existen nombre de institucion y nombre de usuario, verifica que sea el mismo a actualizar
            {
                //Verifica que no exista el nombre de usuario
                var nombre = _context.Usuarios.Where(x => x.Usuario == usuarios.Usuario).FirstOrDefault();

                if (nombre == null || nombre.Id == user.Id)// si no existe el nombre, actualiza
                {
                    user.Usuario = usuarios.Usuario;
                    _context.Update(user);
                    await _context.SaveChangesAsync(); //guarda

                    return 1;

                }
                else // de lo contrario retorna 0
                    return 0;

            }
            else // si envia nombre y contraseña
            {
                user.Usuario = usuarios.Usuario;
                user.Contraseña = usuarios.Contraseña;
                _context.Update(user);
                await _context.SaveChangesAsync(); //guarda
                return 1;
            }
        }

        [HttpPut("editarinstitucion")]
        public async Task<int> Editar_Institucion(UsuarioInstitucion institucion) //metodo para actualizar una asignatura
        {

            //busca el usuario segun el id de la institucion
            var user = _context.UsuariosInstituciones.Where(x => x.IdInstitucion == institucion.Id).FirstOrDefault(); //verifica si existe una institucion

            //busca el usuario la institucion
            var institucio = _context.Institucion.Where(x => x.Id == institucion.Id).FirstOrDefault();

            //si solo cambia el nombre de usuario
            if (institucion.Contraseña == null) // si existen nombre de institucion y nombre de usuario, verifica que sea el mismo a actualizar
            {
                //Verifica que no exista el nombre de usuario
                var nombre = _context.UsuariosInstituciones.Where(x => x.Usuario ==institucion.Usuario).FirstOrDefault();

                if (nombre == null || user.Id == nombre.Id)// si no existe el nombre, actualiza
                {
                    user.Usuario = institucion.Usuario;
                    _context.Update(user);
                    await _context.SaveChangesAsync(); //guarda

                    institucio.Direccion = institucion.Direcccion;
                    institucio.Nombre = institucion.Institucion;
                    _context.Update(institucio);
                    await _context.SaveChangesAsync(); //guarda

                    return 1;

                }
                else // de lo contrario retorna 0
                    return 0;

            }
            else // si hay cambios en la contraseña
            {
                user.Usuario = institucion.Usuario;
                user.Contraseña = institucion.Contraseña;
                _context.Update(user);
                await _context.SaveChangesAsync(); //guarda

                institucio.Direccion = institucion.Direcccion;
                institucio.Nombre = institucion.Institucion;
                _context.Update(institucio);
                await _context.SaveChangesAsync(); //guarda

                return 1;
            }
        }


        [HttpGet("UsuariosADMIN")] //Metodo para listar todos los usuarios docentes (ADMINISTRADOR)

        public async Task<ActionResult<List<UsuariosADMIN>>> Usuarios()
        {

            var data = new List<UsuariosADMIN>();

            data = (from item in _context.Usuarios.ToList()
                    join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                    join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                    where item.Cedula == item3.Cedula 
                    select new UsuariosADMIN
                    {
                        Id=item.Id,
                        Nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                        Institucion = item2.Nombre

                    }).ToList();

            return data;
        }


        [HttpGet("DatosUsuariosADMIN")] //Metodo para listar todos los usuarios docentes (ADMINISTRADOR)

        public async Task<ActionResult<DatosUsuariosADMIN>> DatosUsuarios(Busqueda id)
        {

            DatosUsuariosADMIN data = new DatosUsuariosADMIN();

            data = (from item in _context.Usuarios.ToList()
                    join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                    join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                    where item.Cedula == item3.Cedula  && item.Id==id.Id
                    select new DatosUsuariosADMIN
                    {
                        
                        Nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                        Institucion = item2.Nombre,
                        Cedula=item3.Cedula,
                        Direccion=item3.Direccion,
                        Correo=item3.Correo,
                        Sexo=item3.Sexo,
                        FechaNacimiento=item3.FechaNacimiento,
                        Telefono=item3.Telefono

                    }).FirstOrDefault();

            return data;
        }

    }
}
