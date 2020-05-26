using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class AutenticarController : Controller
    {
        private readonly RedesDeSolidaridadContext _context;

        public AutenticarController(RedesDeSolidaridadContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }
        [TempData]
        public string[] Usuario { get; set; }
        [HttpPost]
        public async Task<IActionResult> Autenticacion([Bind("username,password")] userview usuario)
        {
            Usuario = new string[6];

            var valor = -1;

            //si el UsUARIO ES EL ADMINISTRADOR entonces entra de lo contrario Verifica las Credenciales en la base de datos
            if (usuario.username == "ADMIN" && usuario.password == "Admin123")
            {
                valor = 1;
                Usuario[0] = "ADMIN";
                Usuario[1] = "Administrador";
                Usuario[2] = " ";
                Usuario[3] = " ";
                Usuario[4] = "1";
                Usuario[5] = "eduNica";
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
                    valor = 1;
                    foreach (usuariosview user in Data)
                    {
                        Usuario[0] = user.NombreDeUsuario;
                        Usuario[1] = user.Nombre;
                        Usuario[2] = user.Cedula;
                        Usuario[3] = user.Id;
                        Usuario[4] = user.tipo.ToString();
                        Usuario[5] = user.Institucion;
                    }
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
                        valor = 1;
                        foreach (usuariosview user in Data2)
                        {
                            Usuario[0] = user.NombreDeUsuario;
                            Usuario[1] = user.Nombre;
                            Usuario[2] = user.Cedula;
                            Usuario[3] = user.Id;
                            Usuario[4] = user.tipo.ToString();
                            Usuario[5] = user.Institucion;
                        }
                    }

                }
            }
            return Json(valor);
        }
    }
}
