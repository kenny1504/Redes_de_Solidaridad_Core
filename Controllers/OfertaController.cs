using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;

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
                            idoferta=item2.Id,
                            Descripcion= item2.Descripcion,
                            Fecha=item2.FechaOferta.Year,
                            Docente=item7.Nombre+" "+ item7.Apellido1+" "+ item7.Apellido2,
                            Grado=item4.Grado,
                            Grupo=item3.Grupo,
                            Seccion=item5.Codigo
                        }).ToList();

            return Json(data);
        }
    }
}