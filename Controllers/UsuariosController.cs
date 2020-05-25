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
        //Declaracion de varible para manejo de sesiones
        [TempData]
        public string[] Usuario { get; set; }


        public IActionResult Index() //Envia vista de inicio de sesion
        {

            return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }

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
                                tipo =2,
                                Institucion =i.Nombre
                            }).ToList();
                if (Data.Count > 0) // si encuntra un usuario Guarda el Usuario en cache
                {
                    valor = 1;
                    foreach ( usuariosview user in Data)
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
                                 where usuario.username == u.Usuario && usuario.password==u.Contraseña
                                 select new usuariosview
                                 {
                                     NombreDeUsuario = u.Usuario,
                                     Nombre = i.Nombre,
                                     Cedula = " ",
                                     Id = " ",
                                     tipo =3,
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

        //    // GET: Usuarios/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var usuarios = await _context.Usuarios
        //            .FirstOrDefaultAsync(m => m.IdUsuarios == id);
        //        if (usuarios == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(usuarios);
        //    }

        //    // GET: Usuarios/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Usuarios/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("IdUsuarios,ClaveDeUsuario,Nombre,Cedula,NombreDeUsuario")] Usuarios usuarios)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(usuarios);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(usuarios);
        //    }

        //    // GET: Usuarios/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var usuarios = await _context.Usuarios.FindAsync(id);
        //        if (usuarios == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(usuarios);
        //    }

        //    // POST: Usuarios/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("IdUsuarios,ClaveDeUsuario,Nombre,Cedula,NombreDeUsuario")] Usuarios usuarios)
        //    {
        //        if (id != usuarios.IdUsuarios)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(usuarios);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!UsuariosExists(usuarios.IdUsuarios))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(usuarios);
        //    }

        //    // GET: Usuarios/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var usuarios = await _context.Usuarios
        //            .FirstOrDefaultAsync(m => m.IdUsuarios == id);
        //        if (usuarios == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(usuarios);
        //    }

        //    // POST: Usuarios/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var usuarios = await _context.Usuarios.FindAsync(id);
        //        _context.Usuarios.Remove(usuarios);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool UsuariosExists(int id)
        //    {
        //        return _context.Usuarios.Any(e => e.IdUsuarios == id);
        //    }
        //}
    }
}
