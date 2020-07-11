using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;
using SQLitePCL;

namespace Redes_De_Solidaridad.Controllers
{
    public class NotaController : Controller
    {
        private readonly CentrosEscolares _context;

        public NotaController(CentrosEscolares context)
        {
            _context = context;
        }

        // GET: Asignaturas
        [Route("Notas")]
        public IActionResult Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";
            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "2")//usuario tipo DOCENTE
            {
                return View("~/Areas/Notas/Views/Docente/Mostrar.cshtml");
            }
            else if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Notas/Views/Institucion/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }
        public async Task<IActionResult> Detallenota() //metodo cargar datos en modal
        {
            var data = await _context.Detallenota.ToListAsync();

            return Json(data);
        }

        public async Task<IActionResult> Materias(int idGrado, int IdInstitucion)
        {
            var datos = (from item in _context.Institucion.ToList()
                         join item2 in _context.Detalleasignaturasinstitucion.ToList() on item.Id equals item2.IdInstitucion
                         join item3 in _context.Asignaturas.ToList() on item2.IdAsignatura equals item3.Id
                         join item4 in _context.Gradoasignaturas.ToList() on item3.Id equals item4.AsignaturasId
                         join item5 in _context.Gradoacademico.ToList() on item4.GradoAcademicoId equals item5.Id
                         where item5.Id == idGrado && item.Id == IdInstitucion
                         select new
                         {
                             Id = item3.Id,
                             Nombre = item3.Nombre
                         });
            return Json(datos);
        }

        public async Task<IActionResult> Mostrar_Notas(int año, int idgrado, int idgrupo, int id_detalle_Nota, int id_materia, int idInstitucion)
        {

            //Buscamos la oferta
            var oferta = _context.Ofertas.Where(x => x.FechaOferta.Year == año && x.GradoAcademicoId == idgrado && x.GruposId == idgrupo).FirstOrDefault();

            if (oferta != null)
            {
                var Notas = (from item in _context.Detalleofertasinstitucion.ToList()
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
                             where item.IdInstitucion == idInstitucion && item2.GradoAcademicoId == idgrado && item2.GruposId == idgrupo
                             && item7.DetalleNotaId == id_detalle_Nota && item6.AsignaturasId == id_materia && item3.Fecha.Year == año
                             select new
                             {
                                 id = item7.Id,
                                 CodigoEstudiante = item4.CodigoEstudiante,
                                 Nombre = item5.Nombre + " " + item5.Apellido1 + " " + item5.Apellido2,
                                 Sexo = item5.Sexo,
                                 Grado = item9.Grado,
                                 Grupo = item10.Grupo,
                                 asignatura = item11.Nombre,
                                 Nota = item7.Nota
                             });


                if (Notas.Count() > 0) //Si existen Notas Retorna Datos (Notas)
                {
                    return Json(Notas);
                }
                else //ingresa 0 a todos los estudiantes
                {

                    //Recuperamos los ID de la Tabla detalleMatricula 
                    var Detalle = (from item in _context.Detalleofertasinstitucion.ToList()
                                   join item2 in _context.Ofertas.ToList() on item.IdOferta equals item2.Id
                                   join item3 in _context.Matriculas.ToList() on item2.Id equals item3.OfertasId
                                   join item4 in _context.Detallematricula.ToList() on item3.Id equals item4.MatriculasId
                                   where item2.GradoAcademicoId == idgrado && item2.GruposId == idgrupo && item3.Fecha.Year == año && item4.AsignaturasId == id_materia
                                          && item.IdInstitucion == idInstitucion
                                   select new
                                   {
                                       id = item4.Id
                                   });

                    Notas Nota; //Recorremos la lista de los id detalles y ingresamos notas (0)
                    for (int i = 0; i < Detalle.Count(); i++)
                    {
                        Nota = new Notas();
                        Nota.DetalleNotaId = id_detalle_Nota;
                        Nota.DetalleMatriculaId = Detalle.ElementAt(i).id;
                        Nota.Nota = 0;
                        _context.Add(Nota);
                        await _context.SaveChangesAsync();
                    };

                    //Hacemos nuevamente la cnsulta para ver Notas
                    var Notas2 = (from item in _context.Detalleofertasinstitucion.ToList()
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
                                  where item.IdInstitucion == idInstitucion && item2.GradoAcademicoId == idgrado && item2.GruposId == idgrupo
                                  && item7.DetalleNotaId == id_detalle_Nota && item6.AsignaturasId == id_materia && item3.Fecha.Year == año
                                  select new
                                  {
                                      id = item7.Id,
                                      CodigoEstudiante = item4.CodigoEstudiante,
                                      Nombre = item5.Nombre + " " + item5.Apellido1 + " " + item5.Apellido2,
                                      Sexo = item5.Sexo,
                                      Grado = item9.Grado,
                                      Grupo = item10.Grupo,
                                      asignatura = item11.Nombre,
                                      Nota = item7.Nota
                                  });

                    return Json(Notas2);//Retornamos los datos de la consulta

                }
            }
            else
                return Json(-1); //Si no existe ninguna oferta con los datos proporsionados
        }

        public async Task<IActionResult> Mostrar_Notas_Docente(string cedula, int id_detalle_Nota, int idMateria)
        {

            //Buscamos la oferta
            Ofertas oferta = (from item in _context.Usuarios
                              join item2 in _context.Institucion on item.IdInstitucion equals item2.Id
                              join item3 in _context.Personas on item2.Id equals item3.IdInstitucion
                              join item4 in _context.Docentes on item3.Id equals item4.PersonasId
                              join item5 in _context.Ofertas on item4.Id equals item5.DocentesId
                              where item3.Cedula == cedula && item.Cedula == item3.Cedula && item5.FechaOferta.Year == DateTime.Today.Year
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
                             where item3.OfertasId == oferta.Id && item8.Id == id_detalle_Nota && item11.Id == idMateria
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
                                where item7.Id == oferta.Id && item6.Id==idMateria
                                select new
                                {
                                  id = item5.Id
                                });


                Notas Nota; 
                //Recorremos la lista de los id detalles y ingresamos notas (0)
                for (int i=0; i<SinNotas.Count(); i++)
                {
                   
                   
                        Nota = new Notas();
                        Nota.DetalleNotaId = id_detalle_Nota;
                        Nota.DetalleMatriculaId = SinNotas.ElementAt(i).id;
                        Nota.Nota = 0;
                        _context.Add(Nota);
                        await _context.SaveChangesAsync();
                }

                    //Hacemos nuevamente la consulta para ver Notas
                    var DatosNotas = (from item in _context.Detalleofertasinstitucion.ToList()
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
                                  where item2.Id == oferta.Id && item8.Id == id_detalle_Nota && item11.Id == idMateria
                                  select new
                                  {
                                      id = item7.Id,
                                      CodigoEstudiante = item4.CodigoEstudiante,
                                      Nombre = item5.Nombre + " " + item5.Apellido1 + " " + item5.Apellido2,
                                      Sexo = item5.Sexo,
                                      Grado = item9.Grado,
                                      Grupo = item10.Grupo,
                                      asignatura = item11.Nombre,
                                      Nota = item7.Nota
                                  });

                    return Json(DatosNotas);//Retornamos los datos de la consulta

                
            }
            else
                return Json(-1); //Si no existe ninguna oferta con los datos proporsionados
        }

        //Cargar Materias cuando el docente agregue Notas
        public async Task<IActionResult> MateriasDocente()
        {


            var datos = (from item in _context.Institucion.ToList()
                         join item2 in _context.Detalleasignaturasinstitucion.ToList() on item.Id equals item2.IdInstitucion
                         join item3 in _context.Asignaturas.ToList() on item2.IdAsignatura equals item3.Id
                         join item4 in _context.Gradoasignaturas.ToList() on item3.Id equals item4.AsignaturasId
                         join item5 in _context.Gradoacademico.ToList() on item4.GradoAcademicoId equals item5.Id
                         where item5.Id == Global.idgrado && item.Id == Global.idinstitucion
                         select new
                         {
                             Id = item3.Id,
                             Nombre = item3.Nombre
                         });
            return Json(datos);
        }

        public async Task<IActionResult> AgregarNota(int[] Nota, int[] IdNota)
        {
            Notas notas;
            for (int i = 0; i < Nota.Count(); i++)
            {
                notas = _context.Notas.Where(x => x.Id == IdNota[i]).FirstOrDefault();
                notas.Nota = Nota[i];
                _context.Update(notas);
                await _context.SaveChangesAsync(); //guarda
            }
            return Json(1);
        }
    }
}
