using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class TurnoController : Controller
    {
        private readonly CentrosEscolares _context;

        public TurnoController(CentrosEscolares context)
        {
            _context = context;
        }

        public async Task<IActionResult> Turnos() //metodo cargar datos en modal
        {
            var data = await _context.Turnos.ToListAsync();

            return Json(data);
        }

        [Route("Turnos")]
        public async Task<IActionResult> Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion

            if (usuario != null) //verifica si existe una sesion Valida
            {
                return View("~/Areas/Turno/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

        }
    }
}
