using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

            //verifica que no exista no se repita una asistencia en esa fecha
            var verificar = (from item in _context.Matriculas
                     join item2 in _context.Asistencia on item.Id equals item2.IdMatricula
                     where item2.IdMatricula == dato.ElementAt(0).IdMatricula && item2.Fecha.Date == dato.ElementAt(0).Fecha.Date
                             select new
                     {
                         id = item2.IdMatricula
                     }).Count();
            if(verificar!=0)
            {
                return 0;
            }
            else
            {

                for (int i = 0; i < dato.Count(); i++)
                {
                    _context.Add(dato[i]);
                    await _context.SaveChangesAsync();
                }
                return 1;
            }
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

        //Servicio para recuperar la cantidad de estudiantes segun la institucion 
        [HttpGet("VerAsistencia")]
        public async Task<ActionResult<List<int>>> Ver_Asistencia(Busqueda dato)
        {
            //busca la matricula segun el id matricula recivido
            var busqueda = _context.Matriculas.Where(x => x.Id == dato.Id).FirstOrDefault();


            //consulta para ver cantidad de Asistencias
            var Asistencias = (from a in _context.Asistencia.ToList()
                               join m in _context.Matriculas.ToList() on a.IdMatricula equals m.Id
                               join o in _context.Ofertas.ToList() on m.OfertasId equals o.Id
                               join dm in _context.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                               where o.Id==busqueda.OfertasId && m.Fecha.Year == DateTime.Today.Year
                               group a by new { a.Fecha } into asistencias
                               select new
                               {
                                   fecha = asistencias.Key.Fecha

                               }).Count();

            //consulta para ver cantidad de veces asistidas por el estudiante
            var asistidas = (from a in _context.Asistencia.ToList()
                             join m in _context.Matriculas.ToList() on a.IdMatricula equals m.Id
                             join o in _context.Ofertas.ToList() on m.OfertasId equals o.Id
                             join dm in _context.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                             where m.Id==dato.Id && m.Fecha.Year == DateTime.Today.Year && a.Estado == 1
                             group a by new { a.Fecha } into asistencias
                             select new
                             {
                                 fecha = asistencias.Key.Fecha

                             }).Count();

            //calculo de faltas o ausensias
            var ausencias = Asistencias - asistidas;

            //Crea Lista de Enteros
            var asistencia = new List<int>();
            asistencia.Add(ausencias);//Agrega cantidad de Ausencias
            asistencia.Add(asistidas);//Agrega cantidad de Asistidas

            return asistencia;//Retorna lista

        }

        //Servicio para recuperar la fechas (Ausencias)
        [HttpGet("Fechas")]
        public async Task<List<string>> Fechas(Busqueda dato)
        {
            //busca la matricula segun el id matricula recivido
            var busqueda = _context.Matriculas.Where(x => x.Id == dato.Id).FirstOrDefault();

            //Fechas asistidas
            var asisti = (from a in _context.Asistencia.ToList()
                          join m in _context.Matriculas.ToList() on a.IdMatricula equals m.Id
                          join o in _context.Ofertas.ToList() on m.OfertasId equals o.Id
                          join dm in _context.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                          where m.Id == dato.Id && m.Fecha.Year == DateTime.Today.Year && a.Estado == 1
                          group a by new { a.Fecha } into asistencias
                          select new
                          {
                              fecha = asistencias.Key.Fecha.ToShortDateString()

                          });

            //Fechas Totas de Fechas 
            var Asistencias = (from a in _context.Asistencia.ToList()
                               join m in _context.Matriculas.ToList() on a.IdMatricula equals m.Id
                               join o in _context.Ofertas.ToList() on m.OfertasId equals o.Id
                               join dm in _context.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                               where o.Id == busqueda.OfertasId && m.Fecha.Year == DateTime.Today.Year
                               group a by new { a.Fecha } into asistencias
                               select new
                               {
                                   fecha = asistencias.Key.Fecha.ToShortDateString()

                               });

            //Fechas de Inasistencias
            var ausencia = Asistencias.Where(a => !asisti.Select(l => l.fecha).Contains(a.fecha)).Select(x=> x.fecha).ToList();

            return ausencia;
        }
    }
}
