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

      //  [Route ("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}