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
    public class GradosController : Controller
    {
        private readonly CentrosEscolares _context;

        public GradosController(CentrosEscolares context)
        {
            _context = context;
        }

        public async Task<IActionResult> Grados() //metodo cargar datos en modal
        {
            var data = await _context.Gradoacademico.ToListAsync();

            return Json(data);
        }

        [Route("Grados")]
        public async Task<IActionResult> Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";

            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "1") //usuario tipo ADMINISTRADOR
            {
                return View("~/Areas/Grado/Views/Mostrar.cshtml");
            }
            else if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Grado/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");


        }

    }
}
