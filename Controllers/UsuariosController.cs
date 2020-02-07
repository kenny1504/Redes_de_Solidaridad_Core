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


            var valor =  -1;
            //Consulta a base de datos - Consulta linq
            var Data =(from i in _context.Funcionesasignada
                           join x in _context.Funcionesdeacceso on i.IdFuncionAcceso equals x.IdFuncionAcceso
                           join y in _context.Usuarios on i.IdUsuarios equals y.IdUsuarios
                           where usuario.password ==y.ClaveDeUsuario && usuario.username ==y.NombreDeUsuario
                           select new usuariosview{ 
                               ClaveDeUsuario=y.ClaveDeUsuario,
                               NombreDeUsuario=y.NombreDeUsuario,
                               Nombre=y.Nombre,
                               Cedula=y.Cedula,
                               Descripcion=x.Descripcion,
                               FechaDeVencimiento=i.FechaDeVencimiento,
                               login_in=true

                           }).ToList(); 

            if(Data!=null)
            {
                foreach (usuariosview users in Data) { // recorre arreglo para asignar valores a varible de session
                  
               var fechaUser =DateTime.Parse(users.FechaDeVencimiento.ToString()); //captura fecha del usuario que intenta ingresar
               var anio =fechaUser.Year; // variable que guarda año
               var mes =fechaUser.Month; // variable que guarda mes
               var dia =fechaUser.Day;// variable que guarda dia

                    if (anio > DateTime.Now.Year){ // si el año es mayor a la fecha actual entra

                        Usuario =  new string[7];
                        //Asigna valores a variable de sesion temdata
                        Usuario[0] = users.ClaveDeUsuario;
                        Usuario[1] = users.NombreDeUsuario;
                        Usuario[2] = users.Nombre;
                        Usuario[3] = users.Cedula;
                        Usuario[4] = users.Descripcion;
                        Usuario[5] = users.FechaDeVencimiento.ToString();
                        Usuario[6] =users.login_in.ToString();


                     valor = 1; // retorna 1 si el usuario existe y esta con la fecha de vencimiento valida

                    }
                  else
                    {
                        if (anio == DateTime.Now.Year)//si el año es igual a la fecha actual valida mes y dia
                     {
                            if (mes > DateTime.Now.Month || mes == DateTime.Now.Month) //validacion del mes
                        {
                                if (dia > DateTime.Now.Day || dia == DateTime.Now.Day) //validacion del dia
                           {
                                    //Asigna valores a variable de sesion temdata
                                    Usuario[0] = users.ClaveDeUsuario;
                                    Usuario[1] = users.NombreDeUsuario;
                                    Usuario[2] = users.Nombre;
                                    Usuario[3] = users.Cedula;
                                    Usuario[4] = users.Descripcion;
                                    Usuario[5] = users.FechaDeVencimiento.ToString();
                           
                              valor = 1; // retorna 1 si el usuario existe y esta con la fecha de vencimiento valida
                                }
                           else //si esl dia es menor no ingresa
                                {
                                 valor = 0;
                                }

                            }else //si el mes es menor no ingresa
                            {
                              valor = 0;
                            }
                        }
                     else //si el año es menor no ingresa
                        {
                          valor = 0;
                        }
                    }


                } //foreach  

            }
                return  Json(valor);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuarios == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuarios,ClaveDeUsuario,Nombre,Cedula,NombreDeUsuario")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuarios,ClaveDeUsuario,Nombre,Cedula,NombreDeUsuario")] Usuarios usuarios)
        {
            if (id != usuarios.IdUsuarios)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.IdUsuarios))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuarios == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuarios == id);
        }
    }
}
