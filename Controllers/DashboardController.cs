﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class DashboardController : Controller
    {

        private readonly CentrosEscolares _context;

        public DashboardController(CentrosEscolares context)
        {
            _context = context;
        }
        //Peticiones para Dashboard ADMINISTRADOR
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
            var matriculas = _context.Matriculas.Where(x => x.Fecha.Year == DateTime.Today.Year).Count();

            return Json(matriculas);
        }
        public async Task<IActionResult> Total_Estudiantes()
        {
            var selec = (from i in _context.Institucion
                         join d in _context.Detallematriculainstitucion on i.Id equals d.IdInstitucion
                         join m in _context.Matriculas on d.IdMatricula equals m.Id
                         where m.Fecha.Year == DateTime.Today.Year
                         orderby i.Id descending
                         group i by new { i.Nombre, i.Id } into institucion
                         select new
                         {
                             Nombre = institucion.Key.Nombre,
                             Cantidad = institucion.Count()
                         }).Take(4); //Consulta para recuperar los ultimos  4 registros de las instituciones y su cantidad de matriculas

            return Json(selec);
        }
        public async Task<IActionResult> Total_Grados()
        {
            var selec = (from gr in _context.Gradoacademico
                         join o in _context.Ofertas on gr.Id equals o.GradoAcademicoId
                         join m in _context.Matriculas on o.Id equals m.OfertasId
                         where m.Fecha.Year == DateTime.Today.Year
                         group gr by new { gr.Grado, gr.Id } into grados
                         select new
                         {
                             Grado = grados.Key.Grado,
                             Cantidad = grados.Count()
                         }); //Consulta para recuperar 

            return Json(selec);
        }
        public async Task<IActionResult> Estudiantes_Sexo()
        {
            var selec = (from p in _context.Personas
                         join e in _context.Estudiantes on p.Id equals e.PersonasId
                         join m in _context.Matriculas on e.Id equals m.EstudiantesId
                         where m.Fecha.Year == DateTime.Today.Year
                         group p by new { p.Sexo } into sexo
                         select new
                         {
                             sexo = sexo.Key.Sexo,
                             Cantidad = sexo.Count()
                         }); //Consulta para recuperar 

            return Json(selec);
        }
        public async Task<IActionResult> Tutores_Sexo()
        {
            var selec = (from p in _context.Personas
                         join t in _context.Tutores on p.Id equals t.PersonasId
                         join e in _context.Estudiantes on t.Id equals e.TutorId
                         join m in _context.Matriculas on e.Id equals m.EstudiantesId
                         where m.Fecha.Year == DateTime.Today.Year
                         group p by new { p.Sexo } into sexo
                         select new
                         {
                             sexo = sexo.Key.Sexo,
                             Cantidad = sexo.Count()
                         }); //Consulta para recuperar 

            return Json(selec);
        }

        //Peticiones para Dashboard Institucion
        public async Task<IActionResult> TotalMatriculas_Institucion(uint? id)
        {
            //consulta para ver cantidad de matriculas por la institucion
            var matriculas = (from m in _context.Matriculas
                              join e in _context.Estudiantes on m.EstudiantesId equals e.Id
                              join p in _context.Personas on e.PersonasId equals p.Id
                              where p.IdInstitucion == id && m.Fecha.Year == DateTime.Today.Year
                              select new
                              {
                                  id = m.Id
                              }).Count();

            return Json(matriculas);
        }
        public async Task<IActionResult> TotalEstudiantes_Institucion(uint? id)
        {
            var estudiantes = _context.Personas.Join
                (_context.Estudiantes, p => p.Id, e => e.PersonasId, (p, e) => p)
                .Where(x => x.IdInstitucion == id).Count();

            return Json(estudiantes);
        }
        public async Task<IActionResult> TotalDocente_Institucion(uint? id)
        {
            var docente = _context.Personas.Join
                (_context.Docentes, p => p.Id, d => d.PersonasId, (p, d) => p)
                .Where(x => x.IdInstitucion == id).Count();

            return Json(docente);
        }

        public async Task<IActionResult> Total_Grados_Institucion(uint? id)
        {
            var selec = (from gr in _context.Gradoacademico
                         join o in _context.Ofertas on gr.Id equals o.GradoAcademicoId
                         join m in _context.Matriculas on o.Id equals m.OfertasId
                         join e in _context.Estudiantes on m.EstudiantesId equals e.Id
                         join p in _context.Personas on e.PersonasId equals p.Id
                         where m.Fecha.Year == DateTime.Today.Year && p.IdInstitucion == id
                         group gr by new { gr.Grado, gr.Id } into grados
                         select new
                         {
                             Grado = grados.Key.Grado,
                             Cantidad = grados.Count()
                         }); //Consulta para recuperar 

            return Json(selec);
        }
        public async Task<IActionResult> Estudiantes_Sexo_Institucion(uint? id)
        {
            var selec = (from p in _context.Personas
                         join e in _context.Estudiantes on p.Id equals e.PersonasId
                         join m in _context.Matriculas on e.Id equals m.EstudiantesId
                         where m.Fecha.Year == DateTime.Today.Year && p.IdInstitucion == id
                         group p by new { p.Sexo } into sexo
                         select new
                         {
                             sexo = sexo.Key.Sexo,
                             Cantidad = sexo.Count()
                         }); //Consulta para recuperar 

            return Json(selec);
        }
        public async Task<IActionResult> Tutores_Sexo_Institucion(uint? id)
        {
            var selec = (from p in _context.Personas
                         join t in _context.Tutores on p.Id equals t.PersonasId
                         join e in _context.Estudiantes on t.Id equals e.TutorId
                         join m in _context.Matriculas on e.Id equals m.EstudiantesId
                         where m.Fecha.Year == DateTime.Today.Year && p.IdInstitucion == id
                         group p by new { p.Sexo } into sexo
                         select new
                         {
                             sexo = sexo.Key.Sexo,
                             Cantidad = sexo.Count()
                         }); //Consulta para recuperar 

            return Json(selec);
        }

        // peticioones para Docente

        public async Task<IActionResult> Datosofertas(string cedula)
        {
            var selec = (from item in _context.Usuarios
                         join item2 in _context.Institucion on item.IdInstitucion equals item2.Id
                         join item3 in _context.Personas on item2.Id equals item3.IdInstitucion
                         join item4 in _context.Docentes on item3.Id equals item4.PersonasId
                         join item5 in _context.Ofertas on item4.Id equals item5.DocentesId
                         join item6 in _context.Gradoacademico on item5.GradoAcademicoId equals item6.Id
                         join item7 in _context.Grupos on item5.GruposId equals item7.Id
                         where item3.Cedula == cedula && item.Cedula == item3.Cedula && item5.FechaOferta.Year == DateTime.Today.Year
                         select new
                         {
                             id = item2.Id,
                             idGrado = item6.Id,
                             idGrupo = item7.Id
                         }).ToList(); //Consulta para recuperar 

            for (int i = 0; i < selec.Count(); i++)
            {
                Global.idinstitucion = selec.ElementAt(i).id;
                Global.idgrado = selec.ElementAt(i).idGrado;
                Global.idgrupo = selec.ElementAt(i).idGrupo;
            };

           
            return Json(1);
        }
        public async Task<IActionResult> Docente(string cedula)
        {
            var datos = (from item in _context.Usuarios
                         join item2 in _context.Institucion on item.IdInstitucion equals item2.Id
                         join item3 in _context.Personas on item2.Id equals item3.IdInstitucion
                         join item4 in _context.Docentes on item3.Id equals item4.PersonasId
                         join item5 in _context.Ofertas on item4.Id equals item5.DocentesId
                         join item6 in _context.Gradoacademico on item5.GradoAcademicoId equals item6.Id
                         join item7 in _context.Grupos on item5.GruposId equals item7.Id
                         where item3.Cedula == cedula && item.Cedula == item3.Cedula && item5.FechaOferta.Year == DateTime.Today.Year
                         select new
                         {
                             id = item2.Id,
                             Nombre = item2.Nombre,
                             Grado = item6.Grado,
                             Grupo = item7.Grupo,
                             idGrado = item6.Id,
                             idGrupo = item7.Id
                         });

            return Json(datos);
        }
    }
}
