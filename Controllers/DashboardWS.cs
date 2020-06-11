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
    public class DashboardWS : ControllerBase
    {
        private readonly CentrosEscolares _context;

        public DashboardWS(CentrosEscolares context)
        {
            _context = context;
        }

        [HttpGet("Dashboard/Admin")] //Servicio para llenar grafico
        public async Task<ActionResult<List<Estudiantes_grado>>> Total_Grados()
        {
            var Grados = new List<Estudiantes_grado>(); //Lista de tipo grado

           Grados = (from gr in _context.Gradoacademico
                         join o in _context.Ofertas on gr.Id equals o.GradoAcademicoId
                         join m in _context.Matriculas on o.Id equals m.OfertasId
                         where m.Fecha.Year == DateTime.Today.Year
                         group gr by new { gr.Grado, gr.Id } into grados
                         select new Estudiantes_grado
                         {
                             Grado = grados.Key.Grado,
                             Cantidad = grados.Count()
                         }).ToList(); //Consulta para recuperar 

            return Grados;
        }

    }
}
