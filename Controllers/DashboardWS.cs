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

        [HttpGet("Dashboard/Admin")] //Servicio para llenar grafico Usuario Administrador
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
        [HttpGet("Dashboard/Institucion")] //Servicio para llenar grafico Usuario Institucion
        public async Task<ActionResult<List<Estudiantes_grado>>> Total_Grados(Busqueda inst)
        {
            var Grados = new List<Estudiantes_grado>(); //Lista de tipo grado

            Grados = (from gr in _context.Gradoacademico
                      join o in _context.Ofertas on gr.Id equals o.GradoAcademicoId
                      join m in _context.Matriculas on o.Id equals m.OfertasId
                      join e in _context.Estudiantes on m.EstudiantesId equals e.Id
                      join p in _context.Personas on e.PersonasId equals p.Id
                      where m.Fecha.Year == DateTime.Today.Year && p.IdInstitucion == inst.Id
                      group gr by new { gr.Grado, gr.Id } into grados
                      select new Estudiantes_grado
                      {
                          Grado = grados.Key.Grado,
                          Cantidad = grados.Count()
                      }).ToList(); //Consulta para recuperar 

            return Grados;
        }

        [HttpGet("Dashboard/Datos_Admin")] //Servicio para llenar Cuadros de Dashboard
        public async Task<ActionResult<DasboardWS>> DatosAdmin()
        {
            var Datos = new DasboardWS();

            var estudiantes = _context.Estudiantes.Count();
            var institucion = _context.Institucion.Count(); 
            var docente = _context.Docentes.Count();
            var matriculas = _context.Matriculas.Where(x => x.Fecha.Year == DateTime.Today.Year).Count();

            Datos.Docentes = docente;
            Datos.Estudiantes = estudiantes;
            Datos.Instituciones = institucion;
            Datos.Matriculas = matriculas;

            return Datos;
        }

        [HttpGet("Dashboard/Datos_Institucion")] //Servicio para llenar Cuadros de Dashboard
        public async Task<ActionResult<DasboardWS>> DatosInstitucion(Busqueda inst)
        {
            var Datos = new DasboardWS();

            //consulta para ver cantidad de matriculas por la institucion
            var matriculas = (from m in _context.Matriculas
                               join e in _context.Estudiantes on m.EstudiantesId equals e.Id
                               join p in _context.Personas on e.PersonasId equals p.Id
                               where p.IdInstitucion == inst.Id && m.Fecha.Year == DateTime.Today.Year
                              select new
                               {
                                   id = m.Id
                               }).Count();

            //consulta para ver cantidad de estudiantes por la institucion
            var estudiantes = _context.Personas.Join
               (_context.Estudiantes, p => p.Id, e => e.PersonasId, (p, e) => p)
               .Where(x => x.IdInstitucion == inst.Id).Count();

            //consulta para ver cantidad de Docentes por la institucion
            var docente = _context.Personas.Join
                (_context.Docentes, p => p.Id, d => d.PersonasId, (p, d) => p)
                .Where(x => x.IdInstitucion == inst.Id).Count();

            Datos.Docentes = docente;
            Datos.Estudiantes = estudiantes;
            Datos.Matriculas = matriculas;

            return Datos;
        }
    }
}
