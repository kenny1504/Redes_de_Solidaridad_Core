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

            if (usuario != null && usuario[6].ToString() == "True") //verifica si existe una sesion Valida
            {
                return View("~/Areas/LTE/Views/Inicio.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");

            
        }
    }
}