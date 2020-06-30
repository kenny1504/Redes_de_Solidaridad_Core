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

    public class AsistenciaWS : ControllerBase
    {
        private readonly CentrosEscolares _context;

        public AsistenciaWS(CentrosEscolares context)
        {
            _context = context;
        }

        //Servicio para recuperar la cantidad de estudiantes segun la institucion 
        [HttpPost("AgregarAsistencia")]
        public async Task<ActionResult<int>> Agregar_Asistencia(List<Asistencia> dato)
        {
            for (int i = 0; i < dato.Count(); i++)
            {
                _context.Add(dato[i]);
                await _context.SaveChangesAsync();
            }
            return 1;
        }


        //Servicio para recuperar la cantidad de estudiantes segun la institucion 
        [HttpGet("ListaAsistencia")]
        public async Task<ActionResult< List<ListaAsistencia>>> Listar_Asistencia(BuscarAsistencia dato)
        {
            var data  = new List<ListaAsistencia>();

            data = (from item in _context.Personas.ToList()
                                join item2 in _context.Docentes.ToList() on item.Id equals item2.PersonasId
                                join item3 in _context.Ofertas.ToList() on item2.Id equals item3.DocentesId
                                join item4 in _context.Matriculas.ToList() on item3.Id equals item4.OfertasId
                                join item5 in _context.Estudiantes.ToList() on item4.EstudiantesId equals item5.Id
                                join item6 in _context.Personas.ToList() on item5.PersonasId equals item6.Id
                                where item.Cedula == dato.Cedula && item.IdInstitucion == dato.IdInstitucion && item4.Fecha.Year == DateTime.Today.Year
                                select new ListaAsistencia
                                {
                                    IdMatricula=item4.Id,
                                    CodigoEstudinte=item5.CodigoEstudiante,
                                    Nombre= item6.Nombre+" "+item6.Apellido1+" "+item6.Apellido2

                                }).ToList();
            return data;

        }
    }
}
