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
    public class AsignaturasWS : ControllerBase
    {
        private readonly CentrosEscolares _context;

        public AsignaturasWS(CentrosEscolares context)
        {
            _context = context;
        }

        [HttpGet("Asignaturas")] //Metodo para recuperar asignaturas segun la institucion
        public async Task<ActionResult<List<Asignaturas>>> Asignaturas_Intitucion(Busqueda inst)
        {

            var data = new List<Asignaturas>();

                 data = (from item in _context.Detalleasignaturasinstitucion.ToList()
                        join item2 in _context.Asignaturas.ToList() on item.IdAsignatura equals item2.Id
                        where item.IdInstitucion == inst.Id
                        select new Asignaturas
                        {
                            Id = item2.Id,
                            Nombre = item2.Nombre

                        }).ToList();


            return data;
        }

    }
}
