using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class OfertaController : Controller
    {

        private readonly CentrosEscolares db = new CentrosEscolares();

        [Route("Ofertas")] //Ruta a llamar
        public IActionResult Index()
        {

            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";
            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }

            if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Oferta/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");


        }

        [HttpPost]
        public ActionResult Datos(int id) //metodo para cargar datos de ofertas
        {
            var data = (from item in db.Detalleofertasinstitucion.ToList()
                        join item2 in db.Ofertas.ToList() on item.IdOferta equals item2.Id
                        join item3 in db.Grupos.ToList() on item2.GruposId equals item3.Id
                        join item4 in db.Gradoacademico.ToList() on item2.GradoAcademicoId equals item4.Id
                        join item5 in db.Secciones.ToList() on item2.SeccionesId equals item5.Id
                        join item6 in db.Docentes.ToList() on item2.DocentesId equals item6.Id
                        join item7 in db.Personas.ToList() on item6.PersonasId equals item7.Id
                        where item.IdInstitucion == id
                        select new
                        {
                            idoferta = item2.Id,
                            Descripcion = item2.Descripcion,
                            Fecha = item2.FechaOferta.Year,
                            Docente = item7.Nombre + " " + item7.Apellido1 + " " + item7.Apellido2,
                            Grado = item4.Grado,
                            Grupo = item3.Grupo,
                            Seccion = item5.Codigo
                        }).ToList();

            return Json(data);
        }


        [HttpPost]
        public ActionResult Ofertas(int id) //metodo para cargar ofertas en select matricula
        {
            var data = (from item in db.Ofertas.ToList()
                        join item2 in db.Detalleofertasinstitucion.ToList() on item.Id equals item2.IdOferta
                        where item2.IdInstitucion == id
                        select new
                        {
                            id = item.Id,
                            descripcion = item.Descripcion
                        }).ToList();

            return Json(data);
        }

        [HttpPost]
        public ActionResult DetallesOfertas(int id) //metodo para cargar ofertas en select matricula
        {
            var data = (from item in db.Ofertas
                        join item2 in db.Docentes on item.DocentesId equals item2.Id
                        join item3 in db.Personas on item2.PersonasId equals item3.Id
                        join item4 in db.Gradoacademico on item.GradoAcademicoId equals item4.Id
                        join item5 in db.Grupos on item.GruposId equals item5.Id
                        join item6 in db.Secciones on item.SeccionesId equals item6.Id
                        where item.Id == id && item.FechaOferta.Year == DateTime.Today.Year
                        select new
                        {
                            Nombre_Seccion = item6.Codigo,
                            Nombre_Docente = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                            Nombre_Grado = item4.Grado,
                            Nombre_Grupo = item5.Grupo
                        }
                     ).FirstOrDefault();

            return Json(data);

        }

        [HttpPost]
        public async Task<IActionResult> Agregar(string Descripcion, DateTime FechaOferta, int SeccionesId, int GradoAcademicoId, int GruposId, int DocentesId,int Idinstitucion) //metodo para cargar ofertas en select matricula
        {
            //Verifica que no exista una oferta ingresada con los mismos datos
            var datos = db.Ofertas.Where(x => x.GradoAcademicoId == GradoAcademicoId && x.GruposId == GruposId && x.FechaOferta.Year == DateTime.Today.Year).FirstOrDefault();

            if(datos!=null)
            {
                return Json(0);
            }
            else
            {
                Ofertas ofertas = new Ofertas();
                ofertas.Descripcion = Descripcion;
                ofertas.FechaOferta = FechaOferta;
                ofertas.SeccionesId = SeccionesId;
                ofertas.GradoAcademicoId = GradoAcademicoId;
                ofertas.GruposId = GruposId;
                ofertas.DocentesId = DocentesId;
                //Guarda Datos en tabla oferta
                db.Add(ofertas);
                await db.SaveChangesAsync();

                Detalleofertasinstitucion detalleofertasinstitucion = new Detalleofertasinstitucion();
                detalleofertasinstitucion.IdInstitucion = Idinstitucion;
                detalleofertasinstitucion.IdOferta = ofertas.Id;
                //Guarda Datos en tabla Detalle oferta Institucion
                db.Add(detalleofertasinstitucion);
                await db.SaveChangesAsync();


                var data = (from item in db.Detalleofertasinstitucion
                            join item2 in db.Ofertas on item.IdOferta equals item2.Id
                            join item3 in db.Grupos on item2.GruposId equals item3.Id
                            join item4 in db.Gradoacademico on item2.GradoAcademicoId equals item4.Id
                            join item5 in db.Secciones on item2.SeccionesId equals item5.Id
                            join item6 in db.Docentes on item2.DocentesId equals item6.Id
                            join item7 in db.Personas on item6.PersonasId equals item7.Id
                            where item2.Id==ofertas.Id
                            select new
                            {
                                idoferta = item2.Id,
                                Descripcion = item2.Descripcion,
                                Fecha = item2.FechaOferta.Year,
                                Docente = item7.Nombre + " " + item7.Apellido1 + " " + item7.Apellido2,
                                Grado = item4.Grado,
                                Grupo = item3.Grupo,
                                Seccion = item5.Codigo
                            }).ToList();

                return Json(data);
            }

        }

        [HttpPost]
        public async Task<ActionResult> EliminarOferta(int id) //metodo para cargar ofertas en select matricula
        {
           
            //Busca si existe una oferta con matriculas
            var buscar = db.Matriculas.Where(x => x.OfertasId == id).FirstOrDefault();

            if(buscar==null)
            {
                var  oferta = await db.Ofertas.FindAsync(id); //busca la asignatura
                db.Ofertas.Remove(oferta); //Elimina
                await db.SaveChangesAsync(); //Guarda
                return Json(1);
            }
            else
            {
                return Json(0); //si no ya possee matriculas no elimina
            }

        }
        [HttpPost]
        public ActionResult Materiasoferta(int idoferta, int idinstitucion) //metodo para cargar materias segun la oferta
        {
            var data = (from item in db.Detalleasignaturasinstitucion.ToList()
                        join item2 in db.Asignaturas.ToList() on item.IdAsignatura equals item2.Id
                        join item3 in db.Gradoasignaturas.ToList() on item2.Id equals item3.AsignaturasId
                        join item4 in db.Gradoacademico.ToList() on item3.GradoAcademicoId equals item4.Id
                        join item5 in db.Ofertas.ToList() on item4.Id equals item5.GradoAcademicoId
                        where item5.Id == idoferta && item.IdInstitucion == idinstitucion
                        select new
                        {
                            Id = item2.Id,
                            Nombre = item2.Nombre
                        }
                ).ToList();

            return Json(data);

        }
        [HttpPost]
        public async Task<IActionResult> situacionMatricula() //metodo para cargar situcion de matricula en ventana modal
        {
            var data = await db.Situacionmatricula.ToListAsync();

            return Json(data);
        }

    }


}
