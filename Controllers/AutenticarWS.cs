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

        private readonly RedesDeSolidaridadContext _context;

        public AutenticarWS(RedesDeSolidaridadContext context)
        {
            _context = context;
        }


        [HttpGet ("login") ]

        public async Task<ActionResult<int>> login (userview usuario)
        {
            var valor=-1;

            //si el UsUARIO ES EL ADMINISTRADOR entonces entra de lo contrario Verifica las Credenciales en la base de datos
            if (usuario.username == "ADMIN" && usuario.password == "Admin123")
            {
                valor = 1;
            }
            else
            {
                //Consulta a base de datos - Consulta linq *Consulta si es Docente
                var Data = (from u in _context.Usuarios
                            join i in _context.Institucion on u.IdInstitucion equals i.Id
                            join p in _context.Personas on i.Id equals p.IdInstitucion
                            where usuario.username == u.Usuario && usuario.password == u.Contraseña
                            select new usuariosview
                            {
                                NombreDeUsuario = u.Usuario,
                                Nombre = p.Nombre + " " + p.Apellido1,
                                Cedula = p.Cedula,
                                Id = u.Cedula,
                                tipo = 2,
                                Institucion = i.Nombre
                            }).ToList();
                if (Data.Count > 0) // si encuntra un usuario Guarda el Usuario en cache
                {
                    valor = 2;
                   
                }
                else
                {
                    //Consulta a base de datos - Consulta linq *Consulta si es Institucion
                    var Data2 = (from u in _context.UsuariosInstituciones
                                 join i in _context.Institucion on u.IdInstitucion equals i.Id
                                 where usuario.username == u.Usuario && usuario.password == u.Contraseña
                                 select new usuariosview
                                 {
                                     NombreDeUsuario = u.Usuario,
                                     Nombre = i.Nombre,
                                     Cedula = " ",
                                     Id = " ",
                                     tipo = 3,
                                     Institucion = i.Nombre
                                 }).ToList();
                    if (Data2.Count > 0) // si encuntra un usuario Guarda el Usuario en cache
                    {
                        valor = 3;
                      
                    }

                }
            }
            return valor;

        }


    }
}
