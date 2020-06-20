﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                    group item5 by new { item5.Id,item5.Nombre} into Asignaturas
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

            data = (from item in _context.Institucion.ToList()
                    join item1 in _context.Personas.ToList() on item.Id equals item1.IdInstitucion
                    join item2 in _context.Estudiantes.ToList() on item1.Id equals item2.PersonasId
                    join item3 in _context.Matriculas.ToList() on item2.Id equals item3.EstudiantesId
                    join item4 in _context.Ofertas.ToList() on item3.OfertasId equals item4.Id
                    join item5 in _context.Detallematricula.ToList() on item4.Id equals item5.MatriculasId
                    join item6 in _context.Asignaturas.ToList() on item5.AsignaturasId equals item6.Id
                    join item7 in _context.Notas.ToList() on item5.Id equals item7.DetalleMatriculaId
                    join item8 in _context.Detallenota.ToList() on item7.DetalleNotaId equals item8.Id
                    where item4.GradoAcademicoId==dato.IdGrado && item4.GruposId==dato.IdGrupo && item1.IdInstitucion==dato.IdInstitucion &&
                    item6.Id==dato.IdAsignatura && item8.Id==dato.IdDetalleNota && item3.Fecha.Year == DateTime.Today.Year //valida que es año sea igual al año actual
                    select new Notas_Estudiante
                    {
                        Id=item2.Id,
                        Nombre=item1.Nombre+" "+item1.Apellido1+" "+ item1.Apellido2,
                        Nota=item7.Nota

                    }).ToList();

            return data;

        }
    }
}
