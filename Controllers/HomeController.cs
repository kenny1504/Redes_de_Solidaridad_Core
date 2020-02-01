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


        [TempData]
        public string usuario { get; set; }

        //  [Route ("index")]
        public IActionResult Index()
        {
            usuario = "kenny 1504";
            return View();
        }
    }
}