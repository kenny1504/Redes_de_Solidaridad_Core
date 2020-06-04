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
                        where item.Cedula == item3.Cedula && item.IdInstitucion==inst.Id
                        select new usuariosWS
                        {
                                NombreDeUsuario =item.Usuario,
                                Nombre= item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                                Id=item.Id,
                                tipo=2,
                                Cedula=item.Cedula,
                               Institucion=item2.Nombre

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
                             Cedula=item3.Cedula,
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


    }
}
