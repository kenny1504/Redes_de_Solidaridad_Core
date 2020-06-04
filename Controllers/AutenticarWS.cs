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
    public class AutenticarWS : ControllerBase
    {

        private readonly CentrosEscolares _context;

        public AutenticarWS(CentrosEscolares context)
        {
            _context = context;
        }


        [HttpGet ("login") ]

        public async Task<ActionResult<List<usuariosview>>> login (userview usuario)
        {
            var data = new List<usuariosview>();

            //si el UsUARIO ES EL ADMINISTRADOR entonces entra de lo contrario Verifica las Credenciales en la base de datos
            if (usuario.username == "ADMIN" && usuario.password == "Admin123")
            {
                usuariosview usuariosview = new usuariosview();

                usuariosview.NombreDeUsuario = "ADMIN";
                usuariosview.Nombre = "eduNica";
                usuariosview.Cedula = "000-000000-0000F";
                usuariosview.Id = "0";
                usuariosview.tipo = 1;
                usuariosview.Institucion = "eduNica";
                usuariosview.Id_Institucion = -1;
                data.Add(usuariosview);
            }
            else
            {
                //Consulta a base de datos - Consulta linq *Consulta si es Docente
                data = ( from u in _context.Usuarios
                            join i in _context.Institucion on u.IdInstitucion equals i.Id
                            join p in _context.Personas on i.Id equals p.IdInstitucion
                            where usuario.username == u.Usuario && usuario.password == u.Contraseña && u.Cedula==p.Cedula
                            select new usuariosview
                            {
                                NombreDeUsuario = u.Usuario,
                                Nombre = p.Nombre + " " + p.Apellido1,
                                Cedula = p.Cedula,
                                Id = u.Cedula,
                                tipo = 2,
                                Institucion = i.Nombre,
                                Id_Institucion=u.IdInstitucion
                            }).ToList();
                if (data.Count == 0) // si encuntra un usuario Guarda el Usuario en cache  ...
                {
                    data= (from u in _context.UsuariosInstituciones
                           join i in _context.Institucion on u.IdInstitucion equals i.Id
                           where usuario.username == u.Usuario && usuario.password == u.Contraseña
                           select new usuariosview
                           {
                               NombreDeUsuario = u.Usuario,
                               Nombre = i.Nombre,
                               Cedula = " ",
                               Id = " ",

                               tipo = 3,
                               Institucion = i.Nombre,
                               Id_Institucion=u.IdInstitucion
                           }).ToList();
                }              
            }
            return data;

        }


    }
}
