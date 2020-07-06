using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class SeccionesController : Controller
    {
        private readonly CentrosEscolares _context;

        public SeccionesController(CentrosEscolares context)
        {
            _context = context;
        }


        public async Task<IActionResult> Secciones() //metodo cargar datos en modal
        {
            var data = await _context.Secciones.ToListAsync();

            return Json(data);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
