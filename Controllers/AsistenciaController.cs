﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly CentrosEscolares db = new CentrosEscolares();

        [Route("Asistencia")]
        public IActionResult Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";

            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "2") //Usuario tipo Docente
            {
                return View("~/Areas/Asistencia/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

            
        }

        [Route("AgregarAsistencia")]
        public IActionResult Index2()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";

            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "2") //Usuario tipo Docente
            {
                return View("~/Areas/Asistencia/Views/Agregar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }


        //Peticiones Ver asistencias de estudiantes
        public async Task<IActionResult> Ver_Asistencia(int id, int grupo, int grado)
        {
            //consulta para ver cantidad de Asistencias
            var Asistencias = (from a in db.Asistencia.ToList()
                               join m in db.Matriculas.ToList() on a.IdMatricula equals m.Id
                               join o in db.Ofertas.ToList() on m.OfertasId equals o.Id
                               join dm in db.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                               where o.GradoAcademicoId == grado && o.GruposId == grupo && m.Fecha.Year == DateTime.Today.Year 
                               group a by new { a.Fecha } into asistencias
                               select new
                               {
                                   fecha = asistencias.Key.Fecha

                               }).Count();

            //consulta para ver cantidad de veces asistidas por el estudiante
            var asistidas = (from a in db.Asistencia.ToList()
                             join m in db.Matriculas.ToList() on a.IdMatricula equals m.Id
                             join o in db.Ofertas.ToList() on m.OfertasId equals o.Id
                             join dm in db.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                             where m.EstudiantesId == id && m.Fecha.Year == DateTime.Today.Year && a.Estado == 1
                             group a by new { a.Fecha } into asistencias
                             select new
                             {
                                 fecha = asistencias.Key.Fecha

                             }).Count();

            //calculo de faltas o ausensias
            var ausencias = Asistencias - asistidas;

            int[] asis = { ausencias, asistidas };

            return Json(asis);
        }

        public async Task<IActionResult> Fechas(int id, int grupo, int grado)
        {
            var asisti = (from a in db.Asistencia.ToList()
                          join m in db.Matriculas.ToList() on a.IdMatricula equals m.Id
                          join o in db.Ofertas.ToList() on m.OfertasId equals o.Id
                          join dm in db.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                          where a.Estado == 1 && m.EstudiantesId==id && m.Fecha.Year == DateTime.Today.Year
                          group a by new { a.Fecha } into asistencias
                          select new
                          {
                              fecha = asistencias.Key.Fecha.ToShortDateString()

                          });

          var Asistencias = (from a in db.Asistencia.ToList()
                               join m in db.Matriculas.ToList() on a.IdMatricula equals m.Id
                               join o in db.Ofertas.ToList() on m.OfertasId equals o.Id
                               join dm in db.Detalleofertasinstitucion.ToList() on o.Id equals dm.IdOferta
                               where  o.GradoAcademicoId == grado && o.GruposId == grupo && m.Fecha.Year == DateTime.Today.Year
                               group a by new { a.Fecha} into asistencias
                               select new
                               {
                                   fecha = asistencias.Key.Fecha.ToShortDateString()

                               });
        
          var ausencia = Asistencias.Where(a => !asisti.Select(l => l.fecha).Contains(a.fecha));

            return Json(ausencia);
        }

        public async Task<ActionResult> Datos(string cedula)// metodo ajax para recuperar datos de estudiantes (Institucion)
        {
            //Buscamos el Id del Docente
            var docente = (from item in db.Personas
                           join item2 in db.Docentes on item.Id equals item2.PersonasId
                           where item.Cedula == cedula
                           select new Docentes
                           {
                               Id = item2.Id
                           }
                           ).FirstOrDefault();

            var data = (from item2 in db.Docentes.ToList()
                        join item3 in db.Ofertas.ToList() on item2.Id equals item3.DocentesId
                        join item4 in db.Matriculas.ToList() on item3.Id equals item4.OfertasId
                        join item5 in db.Estudiantes.ToList() on item4.EstudiantesId equals item5.Id
                        join item6 in db.Personas.ToList() on item5.PersonasId equals item6.Id
                        join item7 in db.Tutores.ToList() on item5.TutorId equals item7.Id
                        join item8 in db.Personas.ToList() on item7.PersonasId equals item8.Id
                        join item9 in db.Gradoacademico.ToList() on item3.GradoAcademicoId equals item9.Id
                        join item10 in db.Grupos.ToList() on item3.GruposId equals item10.Id
                        where item2.Id == docente.Id && item4.Fecha.Year == DateTime.Today.Year
                        select new
                        {
                            IdEstudiante = item5.Id,
                            IdGrupo = item10.Id,
                            IdGrado = item9.Id,
                            Codigo = item5.CodigoEstudiante,
                            Nombre = item6.Nombre + " " + item6.Apellido1 + " " + item6.Apellido2,
                            sexo = item6.Sexo,
                            direccion = item6.Direccion,
                            tutor = item8.Nombre + " " + item8.Apellido1 + " " + item8.Apellido2,

                        });

            return Json(data);
        }

        public async Task<IActionResult> AgregarAsistencia( int [] idMatricula, int [] asistencia)
        {
            var p = 0;
            //verifica que no exista no se repita una asistencia en esa fecha
            var verificar = (from item in db.Matriculas
                             join item2 in db.Asistencia on item.Id equals item2.IdMatricula
                             where item2.IdMatricula == idMatricula[0] && item2.Fecha.Date == DateTime.Today.Date
                             select new
                             {
                                 id = item2.IdMatricula
                             }).Count();
            if (verificar != 0)
            {
                return Json(0);
            }
            else
            {
                var fecha = DateTime.Today.Date;
                Asistencia Asistencias;
                for (int i = 0; i < idMatricula.Count(); i++)
                {
                    Asistencias = new Asistencia();
                    Asistencias.Estado = (ulong)asistencia[i];
                    Asistencias.Fecha = fecha;
                    Asistencias.IdMatricula = idMatricula[i];
                    db.Add(Asistencias);
                    await db.SaveChangesAsync();
                }
                return Json(1);
            }
        }
    
    }


}
