using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AsistenciaWS : ControllerBase
    {
        private readonly CentrosEscolares _context;

        public AsistenciaWS(CentrosEscolares context)
        {
            _context = context;
        }

        //Servicio para recuperar la cantidad de estudiantes segun la institucion 
        [HttpPost("AgregarAsistencia")]
        public async Task<ActionResult<int>> Agregar_Asistencia(List<Asistencia> dato)
        {
            for (int i = 0; i < dato.Count(); i++)
            {
                _context.Add(dato[i]);
                await _context.SaveChangesAsync();
            }
            return 1;
        }
    }
}

