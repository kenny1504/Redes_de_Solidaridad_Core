using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Redes_De_Solidaridad.Areas.LTE.Controllers
{
   // [Area ("LTE")]
   // [Route ("home")]
    public class HomeController : Controller
    {


         [Route ("Inicio")]
        public IActionResult Index()
        {
            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion


            string tipo = (string)usuario[4];//conversiona  entero

            if(usuario != null && tipo =="1") //usuario tipo ADMINISTRADOR
            {
                return View("~/Areas/LTE/Views/Inicio_Admin.cshtml");
            }
            else if (usuario != null && tipo == "2")//usuario tipo DOCENTE
            { 
                return View("~/Areas/LTE/Views/Inicio_Docente.cshtml");
            }
            else if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/LTE/Views/Inicio_Institucion.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

            
        }
    }
}