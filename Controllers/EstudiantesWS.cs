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
    public class EstudiantesWS : ControllerBase
    {
            private readonly CentrosEscolares _context;

            public EstudiantesWS(CentrosEscolares context)
            {
                _context = context;
            }


            //Servicio para recuperar la cantidad de estudiantes segun la institucion 
            [HttpGet("institucion/grado")]

            public async Task<ActionResult<List<Estudiantes_grado>>> Estudiantes_grados(Busqueda inst)
            {
                var data = new List<Estudiantes_grado>();

                data = (from item6 in _context.Ofertas.ToList()
                        join item1 in _context.Matriculas.ToList() on item6.Id equals item1.OfertasId
                        join item2 in _context.Estudiantes.ToList() on item1.EstudiantesId equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.PersonasId equals item3.Id
                        join item4 in _context.Institucion.ToList() on item3.IdInstitucion equals item4.Id
                        join item5 in _context.Gradoacademico.ToList() on item6.GradoAcademicoId equals item5.Id
                        where item4.Id==inst.Id && item1.Fecha.Year==DateTime.Today.Year //valida que es año sea igual al año actual
                        group item5 by new { item5.Id, item5.Grado } into grados
                        select new Estudiantes_grado
                        {
                            Grado=grados.Key.Grado,
                            Cantidad=grados.Count()

                        }).ToList();

              return data;
 
            }

            //Servicio para recuperar la cantidad de grupos segun un grado segun la institucion 
            [HttpGet("institucion/grupos")]

            public async Task<ActionResult<List<Gruposws>>> grupos_grados(Grupos_ws inst)
            {
                var data = new List<Gruposws>();

                data = (from item6 in _context.Ofertas.ToList()
                        join item1 in _context.Matriculas.ToList() on item6.Id equals item1.OfertasId
                        join item2 in _context.Estudiantes.ToList() on item1.EstudiantesId equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.PersonasId equals item3.Id
                        join item4 in _context.Institucion.ToList() on item3.IdInstitucion equals item4.Id
                        join item5 in _context.Gradoacademico.ToList() on item6.GradoAcademicoId equals item5.Id
                        join item7 in _context.Grupos.ToList() on item6.GruposId equals item7.Id
                        where item4.Id == inst.institucion && item5.Id==inst.Grado && item1.Fecha.Year == DateTime.Today.Year //valida que es año sea igual al año actual
                        group item7 by new { item7.Id, item7.Grupo } into grupo
                        select new Gruposws
                        {
                            Grupo = grupo.Key.Grupo,
                            Cantidad = grupo.Count()

                        }).ToList();

                return data;

            }

        [HttpGet("institucion/ListaEstudiantes")]

        public async Task<ActionResult<List<ListaEstudiantesWS>>> Estudiantes_Institucion(estudianteWS dato)
        {
            var data = new List<ListaEstudiantesWS>();

            data = (from item6 in _context.Ofertas.ToList()
                    join item1 in _context.Matriculas.ToList() on item6.Id equals item1.OfertasId
                    join item2 in _context.Estudiantes.ToList() on item1.EstudiantesId equals item2.Id
                    join item3 in _context.Personas.ToList() on item2.PersonasId equals item3.Id
                    join item4 in _context.Institucion.ToList() on item3.IdInstitucion equals item4.Id
                    join item5 in _context.Gradoacademico.ToList() on item6.GradoAcademicoId equals item5.Id
                    join item7 in _context.Grupos.ToList() on item6.GruposId equals item7.Id
                    where item4.Id == dato.IdInstitucion && item5.Id == dato.IdGrado && item7.Id==dato.IdGrupo && item1.Fecha.Year == DateTime.Today.Year //valida que es año sea igual al año actual
                    select new ListaEstudiantesWS
                    {
                        Nombre= item3.Nombre+" "+item3.Apellido1 +" "+item3.Apellido2,
                        Sexo=item3.Sexo

                    }).ToList();

            return data;

        }

        [HttpGet("institucion/DatosEstudiante")]

        public async Task<ActionResult<List<DatosWS>>> Datos_Institucion(estudianteWS dato)
        {
            var data = new List<DatosWS>();

            data = (from item6 in _context.Ofertas.ToList()
                    join item1 in _context.Matriculas.ToList() on item6.Id equals item1.OfertasId
                    join item2 in _context.Estudiantes.ToList() on item1.EstudiantesId equals item2.Id
                    join item3 in _context.Personas.ToList() on item2.PersonasId equals item3.Id
                    join item4 in _context.Institucion.ToList() on item3.IdInstitucion equals item4.Id
                    join item5 in _context.Gradoacademico.ToList() on item6.GradoAcademicoId equals item5.Id
                    join item7 in _context.Grupos.ToList() on item6.GruposId equals item7.Id
                    join item8 in _context.Tutores.ToList() on item2.TutorId equals item8.Id
                    join item9 in _context.Personas.ToList() on item8.PersonasId equals item9.Id
                    where item4.Id == dato.IdInstitucion && item5.Id == dato.IdGrado && item7.Id == dato.IdGrupo && item2.Id==dato.IdEstudiante && item1.Fecha.Year == DateTime.Today.Year //valida que es año sea igual al año actual
                    select new DatosWS
                    {
                        Nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                        Sexo = item3.Sexo,
                        CodigoEstudiante=item2.CodigoEstudiante,
                        Direccion=item3.Direccion,
                        FechaNacimiento=item3.FechaNacimiento,
                        Tutor=item9.Nombre +" "+ item9.Apellido1 + " " + item9.Apellido2,
                        TelefonoTutor =item9.Telefono

                    }).ToList();

            return data;

        }


    }
    }
