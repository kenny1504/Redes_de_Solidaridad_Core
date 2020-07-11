using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;
using SQLitePCL;

namespace Redes_De_Solidaridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasWS : ControllerBase
    {
        private readonly CentrosEscolares _context;

        public NotasWS(CentrosEscolares context)
        {
            _context = context;
        }

        [HttpGet("Asinaturas_Estudiantes")]
        public async Task<ActionResult<List<Asignaturas>>> Materias(estudianteWS dato)
        {
            var data = new List<Asignaturas>();

            data = (from item6 in _context.Institucion.ToList()
                    join item1 in _context.Detalleofertasinstitucion.ToList() on item6.Id equals item1.IdInstitucion
                    join item2 in _context.Ofertas.ToList() on item1.IdOferta equals item2.Id
                    join item3 in _context.Gradoacademico.ToList() on item2.GradoAcademicoId equals item3.Id
                    join item4 in _context.Gradoasignaturas.ToList() on item3.Id equals item4.GradoAcademicoId
                    join item5 in _context.Asignaturas.ToList() on item4.AsignaturasId equals item5.Id
                    join item7 in _context.Matriculas.ToList() on item2.Id equals item7.OfertasId
                    where item6.Id == dato.IdInstitucion && item3.Id == dato.IdGrado && item2.GruposId == dato.IdGrupo && item7.Fecha.Year == DateTime.Today.Year //valida que es año sea igual al año actual
                    group item5 by new { item5.Id, item5.Nombre } into Asignaturas
                    select new Asignaturas
                    {
                        Id = Asignaturas.Key.Id,
                        Nombre = Asignaturas.Key.Nombre
                    }).ToList();

            return data;

        }

        [HttpGet("Detalles_Notas")]
        public async Task<ActionResult<List<Detallenota>>> DetalleNota()
        {
            var datos = new List<Detallenota>();
            datos = await _context.Detallenota.ToListAsync();

            return datos;
        }

        [HttpGet("VerNotas_Estudiantes")]
        public async Task<ActionResult<List<Notas_Estudiante>>> VerNotas(BusquedaNota dato)
        {
            var data = new List<Notas_Estudiante>();

            data = (from item in _context.Matriculas
                    join item1 in _context.Detallematricula on item.Id equals item1.MatriculasId
                    join item2 in _context.Notas on item1.Id equals item2.DetalleMatriculaId
                    join item3 in _context.Detallenota on item2.DetalleNotaId equals item3.Id
                    join item4 in _context.Ofertas on item.OfertasId equals item4.Id
                    join item5 in _context.Estudiantes on item.EstudiantesId equals item5.Id
                    join item6 in _context.Personas on item5.PersonasId equals item6.Id
                    where item4.GradoAcademicoId == dato.IdGrado && item4.GruposId == dato.IdGrupo && item6.IdInstitucion == dato.IdInstitucion &&
                    item1.AsignaturasId == dato.IdAsignatura && item3.Id == dato.IdDetalleNota && item.Fecha.Year == DateTime.Today.Year //valida que es año sea igual al año actual
                    select new Notas_Estudiante
                    {
                        Id = item5.Id,
                        Nombre = item6.Nombre + " " + item6.Apellido1 + " " + item6.Apellido2,
                        Nota = item2.Nota

                    }).ToList();

            return data;

        }
        
        [HttpPost("AgregarNotas_Estudiantes")] //Metodo Para Agregar Nota
        public async Task<ActionResult<int>> AgregarNota(AgregarNota dato)
        {
            //Busca el detalle Matricula del estudiante matriculado
            var DetalleMatricula = _context.Detallematricula.Where(x => x.AsignaturasId == dato.IdAsigntura && x.MatriculasId == dato.IdMatricula).FirstOrDefault();

            //Verifica si existe ya una Nota en esa Matricula
            var Existe = _context.Notas.Where(X => X.DetalleMatriculaId == DetalleMatricula.Id && X.DetalleNotaId == dato.IdDetalle).FirstOrDefault();

            if (Existe == null) //Si no existe agrega
            {   //Agregar Nota

                Notas Nota = new Notas();

                Nota.Nota = dato.Nota;
                Nota.DetalleNotaId = dato.IdDetalle;
                Nota.DetalleMatriculaId = DetalleMatricula.Id;
                _context.Add(Nota);
                await _context.SaveChangesAsync();
                return 1;
            }
            else // si existe Actualiza
            {
                Existe.Nota = dato.Nota;
                _context.Update(Existe);
                await _context.SaveChangesAsync();
                return 2; //Si retorna 2 es porque actualizo la nota
            }
        }

        [HttpGet("Ver_MateriasDocentes")]
        public async Task<ActionResult<List<Asignaturasdocente>>> Materias_Docente(BusquedaUD dato)
        {
            var data = new List<Asignaturasdocente>();

            data = (from item in _context.Personas.ToList()
                    join item2 in _context.Docentes.ToList() on item.Id equals item2.PersonasId
                    join item3 in _context.Ofertas.ToList() on item2.Id equals item3.DocentesId
                    join item4 in _context.Gradoacademico.ToList() on item3.GradoAcademicoId equals item4.Id
                    join item5 in _context.Gradoasignaturas.ToList() on item4.Id equals item5.GradoAcademicoId
                    join item6 in _context.Asignaturas.ToList() on item5.AsignaturasId equals item6.Id
                    where item.Cedula == dato.Cedula && item3.FechaOferta.Year == DateTime.Today.Year
                    select new Asignaturasdocente
                    {
                        Idasignaturas = item6.Id,
                        Idgrupo = item3.GruposId,
                        Idgrado = item3.GradoAcademicoId,
                        Nombre = item6.Nombre
                    }
            ).ToList();

            return data;

        }


        //Este Metodo se llamara cuando el usuario Docente le de click en Notas
        [HttpPost("VerNotas_Docente")] //Metodo Para Agregar Nota usuario Docente
        public async Task<ActionResult<List<Notas_Estudiante>>> VerNotaDocente(VerNotaDocente dato)
        {

            List<Notas_Estudiante> DatosNotas = new List<Notas_Estudiante>(); //Lista de Datos

            //Buscamos la oferta
            Ofertas oferta = (from item in _context.Usuarios
                              join item2 in _context.Institucion on item.IdInstitucion equals item2.Id
                              join item3 in _context.Personas on item2.Id equals item3.IdInstitucion
                              join item4 in _context.Docentes on item3.Id equals item4.PersonasId
                              join item5 in _context.Ofertas on item4.Id equals item5.DocentesId
                              where item3.Cedula == dato.cedula && item.Cedula == item3.Cedula && item5.FechaOferta.Year == DateTime.Today.Year
                              select new Ofertas
                              {
                                  Id = item5.Id,
                                  FechaOferta = item5.FechaOferta
                              }).FirstOrDefault();

            if (oferta != null)
            {
                //Lista de estudiantes que tienen Notas
                var Notas = (from item3 in _context.Matriculas.ToList()
                             join item4 in _context.Estudiantes.ToList() on item3.EstudiantesId equals item4.Id
                             join item6 in _context.Detallematricula.ToList() on item3.Id equals item6.MatriculasId
                             join item7 in _context.Notas.ToList() on item6.Id equals item7.DetalleMatriculaId
                             join item8 in _context.Detallenota.ToList() on item7.DetalleNotaId equals item8.Id
                             join item11 in _context.Asignaturas.ToList() on item6.AsignaturasId equals item11.Id
                             where item3.OfertasId == oferta.Id && item8.Id == dato.id_detalle_Nota && item11.Id == dato.idMateria
                             select new
                             {
                                 id = item6.Id
                             });


                //Lista de estudiantes que NO tienen Notas
                var SinNotas = (from item3 in _context.Estudiantes.ToList()
                                join item4 in _context.Matriculas.ToList() on item3.Id equals item4.EstudiantesId
                                join item5 in _context.Detallematricula.Where(x => !Notas.Select(l => l.id).Contains(x.Id)).ToList() on item4.Id equals item5.MatriculasId
                                join item6 in _context.Asignaturas.ToList() on item5.AsignaturasId equals item6.Id
                                join item7 in _context.Ofertas.ToList() on item4.OfertasId equals item7.Id
                                where item7.Id == oferta.Id && item6.Id == dato.idMateria
                                select new
                                {
                                    id = item5.Id
                                });


                Notas Nota;
                //Recorremos la lista de los id detalles y ingresamos notas (0)
                for (int i = 0; i < SinNotas.Count(); i++)
                {


                    Nota = new Notas();
                    Nota.DetalleNotaId = dato.id_detalle_Nota;
                    Nota.DetalleMatriculaId = SinNotas.ElementAt(i).id;
                    Nota.Nota = 0;
                    _context.Add(Nota);
                    await _context.SaveChangesAsync();
                }
                //Hacemos consulta para ver Notas
                DatosNotas = (from item in _context.Detalleofertasinstitucion.ToList()
                              join item2 in _context.Ofertas.ToList() on item.IdOferta equals item2.Id
                              join item3 in _context.Matriculas.ToList() on item2.Id equals item3.OfertasId
                              join item4 in _context.Estudiantes.ToList() on item3.EstudiantesId equals item4.Id
                              join item5 in _context.Personas.ToList() on item4.PersonasId equals item5.Id
                              join item6 in _context.Detallematricula.ToList() on item3.Id equals item6.MatriculasId
                              join item7 in _context.Notas.ToList() on item6.Id equals item7.DetalleMatriculaId
                              join item8 in _context.Detallenota.ToList() on item7.DetalleNotaId equals item8.Id
                              join item9 in _context.Gradoacademico.ToList() on item2.GradoAcademicoId equals item9.Id
                              join item10 in _context.Grupos.ToList() on item2.GruposId equals item10.Id
                              join item11 in _context.Asignaturas.ToList() on item6.AsignaturasId equals item11.Id
                              where item2.Id == oferta.Id && item8.Id == dato.id_detalle_Nota && item11.Id == dato.idMateria
                              select new Notas_Estudiante
                              {
                                  Id = item7.Id,
                                  Nombre = item5.Nombre + " " + item5.Apellido1 + " " + item5.Apellido2,
                                  Nota = item7.Nota
                              }).ToList();
                return DatosNotas;//Retornamos 1 si esta t
            }
            else
                return DatosNotas; //Si no existe ninguna oferta con los datos proporsionados

        }

        [HttpPut("AgregarNotaDocente")] //Servicio para Guardar Notas (DOcente)
        public async Task<ActionResult<int>> AgregarNotaDocente( NotasD Nota)
        {
            Notas notas;
            for (int i = 0; i < Nota.IdNota.Length; i++)
            {
                notas = _context.Notas.Where(x => x.Id == Nota.IdNota[i]).FirstOrDefault();
                notas.Nota = Nota.Nota[i];
                _context.Update(notas);
                await _context.SaveChangesAsync(); //guarda
            }
            return 1;

        }
    }
}
