using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class MatriculaController : Controller
    {

        private readonly CentrosEscolares _context;

        public MatriculaController(CentrosEscolares context)
        {
            _context = context;
        }

        [HttpPost] //Metodo Para Matricular un Estudiante
        public async Task<IActionResult> Agregar(int OfertasId, int TurnoId, int SituacionMatriculaId, int EstudiantesId , int[] AsignaturasId)
        { 

            //Verifica si no posee ya una matricula
            var Verifica = _context.Matriculas.Where(x => x.EstudiantesId == EstudiantesId && x.Fecha.Year == DateTime.Today.Year).FirstOrDefault();

            if(Verifica==null)
            {
                Matriculas matriculas = new Matriculas();
                matriculas.EstudiantesId = EstudiantesId;
                matriculas.Fecha = DateTime.Today;
                matriculas.OfertasId = OfertasId;
                matriculas.TurnoId = TurnoId;
                matriculas.SituacionMatriculaId = SituacionMatriculaId;
               
                _context.Add(matriculas);
                await _context.SaveChangesAsync();

                Detallematricula Detalle;
                for(int i=0; i<AsignaturasId.Length; i++)
                {
                    Detalle = new Detallematricula();
                    Detalle.AsignaturasId = AsignaturasId[i];
                    Detalle.MatriculasId = matriculas.Id;

                    _context.Add(Detalle);
                    await _context.SaveChangesAsync();
                }
                return Json(1);
            }
            else
            {
                return Json(Verifica);
            }
        }

     


    }
}
