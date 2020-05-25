using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class GradoaasignaturasController : Controller
    {
        private readonly RedesDeSolidaridadContext _context;

        public GradoaasignaturasController(RedesDeSolidaridadContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Crear([Bind("Id,Gradoid,Asignaturaid")] Gradoaasignaturas gradoaasignaturas) //guarda una materia con un determinado grado
        {
            //verifica que no exista la asignatura en ese grado
            var datos = _context.Gradoaasignaturas.Where(x => x.Asignaturaid == gradoaasignaturas.Asignaturaid && x.Gradoid==gradoaasignaturas.Gradoid ).FirstOrDefault();
            if(datos==null) //si no existe guarda
            {
                _context.Add(gradoaasignaturas);
                _context.SaveChangesAsync();
                return Json(1);
            }
            else
            {
                return Json(-1);
            }
        }

        // POST: Gradoaasignaturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Gradoid,Asignaturaid")] Gradoaasignaturas gradoaasignaturas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradoaasignaturas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Nombre", gradoaasignaturas.Asignaturaid);
            ViewData["Gradoid"] = new SelectList(_context.Grados, "Id", "Id", gradoaasignaturas.Gradoid);
            return View(gradoaasignaturas);
        }

        // GET: Gradoaasignaturas/Edit/5

        // POST: Gradoaasignaturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       
    }
}
