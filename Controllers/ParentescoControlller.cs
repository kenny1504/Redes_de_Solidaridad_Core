using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class ParentescoControlller : Controller
    {
        private readonly CentrosEscolares _context;

        public ParentescoControlller(CentrosEscolares context)
        {
            _context = context;
        }

        [Route("Cargar_P")]
        public async Task<IActionResult> Parentescos() //metodo cargar datos en modal
        {
            var data = await _context.Parentescos.ToListAsync();

            return Json(data);
        }

        [Route("Parentescos_Vista")]
        public async Task<IActionResult> Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null) //verifica si existe una sesion Valida
            {
                return View("~/Areas/Parentesco/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

        }
    }
}
