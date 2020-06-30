﻿using System;
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
            var Existe = _context.Notas.Where(X => X.DetalleMatriculaId == DetalleMatricula.Id && X.DetalleNotaId==dato.IdDetalle).FirstOrDefault();

            if (Existe == null)
            {   //Agregar Nota

                Notas Nota = new Notas();

                Nota.Nota = dato.Nota;
                Nota.DetalleNotaId = dato.IdDetalle;
                Nota.DetalleMatriculaId = DetalleMatricula.Id;
                _context.Add(Nota);
                await _context.SaveChangesAsync();
                return 1;
            }
            else //si Retorna -1 es porque ya existe una Nota
                return -1;
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
                        Idasignaturas=item6.Id,
                        Idgrupo=item3.GruposId,
                        Idgrado=item3.GradoAcademicoId,
                        Nombre=item6.Nombre
                    }
            ).ToList();

            return data;

        }
    }
}
