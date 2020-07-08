using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class MatriculaController : Controller
    {

        private readonly CentrosEscolares _context;

        public MatriculaController(CentrosEscolares context)
        {
            _context = context;
        }

        [HttpPost] //Metodo Para Matricular un Estudiante
        public async Task<IActionResult> Agregar(int OfertasId, int TurnoId, int SituacionMatriculaId, int EstudiantesId , int[] AsignaturasId)
        {



            return Json(1);

        }


    }
}
