﻿using System;
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
                        where item4.Id==inst.Id
                        group item5 by new { item5.Id, item5.Grado } into grados
                        select new Estudiantes_grado
                        {
                            Grado=grados.Key.Grado,
                            Cantidad=grados.Count()

                        }).ToList();

            return data;
 
            }
        }
    }
