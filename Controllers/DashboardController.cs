using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;

namespace Redes_De_Solidaridad.Controllers
{
    public class DashboardController : Controller
    {

        private readonly CentrosEscolares _context;

        public DashboardController(CentrosEscolares context)
        {
            _context = context;
        }

        public async Task<IActionResult> TotalEstudiantes()
        {
            var estudiantes = _context.Estudiantes.Count();

            return Json(estudiantes);
        }
        public async Task<IActionResult> TotalInstituciones()
        {
            var institucion = _context.Institucion.Count();

            return Json(institucion);
        }
        public async Task<IActionResult> TotalDocente()
        {
            var docente = _context.Docentes.Count();

            return Json(docente);
        }
        public async Task<IActionResult> TotalMatriculas()
        {
            var docente = _context.Matriculas.Where(x => x.Fecha.Year == DateTime.Today.Year).Count();

            return Json(docente);
        }
        public async Task<IActionResult> Total_Estudiantes()
        {
            var selec = (from i in _context.Institucion
                         join d in _context.Detallematriculainstitucion on i.Id equals d.IdInstitucion
                         join m in _context.Matriculas on d.IdMatricula equals m.Id
                         where m.Fecha.Year==DateTime.Today.Year
                         orderby i.Id descending
                         group i by new {i.Nombre,i.Id} into institucion
                         select new
                         {
                            Nombre=institucion.Key.Nombre,
                            Cantidad=institucion.Count()
                         }).Take(4); //Consulta para recuperar los ultimos  4 registros de las instituciones y su cantidad de matriculas

            return Json(selec);
        }


    }
}
