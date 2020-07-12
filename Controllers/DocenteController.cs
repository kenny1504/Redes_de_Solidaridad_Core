using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Redes_De_Solidaridad.Context;
using Redes_De_Solidaridad.Models;

namespace Redes_De_Solidaridad.Controllers
{
    public class DocenteController : Controller
    {
        private readonly CentrosEscolares _context;

        public DocenteController(CentrosEscolares context)
        {
            _context = context;
        }
        public async Task<IActionResult> Datos_DocenteU(uint? id)
        {

            var data = (from item in _context.Usuarios.ToList()
                        join item2 in _context.Institucion.ToList() on item.IdInstitucion equals item2.Id
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        where item.Id == id && item.Cedula == item3.Cedula
                        select new
                        {
                            cedula = item3.Cedula,
                            nombre = item3.Nombre,
                            apellido1 = item3.Apellido1,
                            apellido2 = item3.Apellido2,
                            sexo = item3.Sexo,
                            direccion = item3.Direccion,
                            correo = item3.Correo,
                            telefono = item3.Telefono,
                            fecha = item3.FechaNacimiento,
                            estado = item4.Estado
                        }).ToList();

            return Json(data);

        }
        public async Task<IActionResult> Mostrar(uint? id)
        {

            var data = (from item2 in _context.Institucion.ToList()
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        where item2.Id == id
                        select new
                        {
                            id = item4.Id,
                            cedula = item3.Cedula,
                            nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                            sexo = item3.Sexo,
                            correo = item3.Correo,
                            telefono = item3.Telefono,
                            estado = item4.Estado
                        });

            return Json(data);

        }
        //Metodo para cargar datos de los docentes en select Oferta
        public async Task<IActionResult> MostrarDocentes(int id)
        {

            var lista = _context.Ofertas.Join
               (_context.Detalleofertasinstitucion.Where(i => i.IdInstitucion == id), o => o.Id, d => d.IdOferta, (p, d) => p)
               .Where(x => x.FechaOferta.Year == DateTime.Today.Year).ToList();


            var data = (from item2 in _context.Institucion.ToList()
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes on item3.Id equals item4.PersonasId
                        join item5 in _context.Ofertas.Where(x => !lista.Select(l => l.DocentesId).Contains(x.DocentesId)).ToList() on item4.Id equals item5.DocentesId
                        where item4.Estado == 1 && item5.FechaOferta.Year == DateTime.Today.Year
                        select new
                        {
                            id = item4.Id,
                            nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                        });

            return Json(data);

        }

        public async Task<IActionResult> MostrarADMIN(uint? id)
        {

            var data = (from item2 in _context.Institucion.ToList()
                        join item3 in _context.Personas.ToList() on item2.Id equals item3.IdInstitucion
                        join item4 in _context.Docentes.ToList() on item3.Id equals item4.PersonasId
                        select new
                        {
                            id = item4.Id,
                            cedula = item3.Cedula,
                            nombre = item3.Nombre + " " + item3.Apellido1 + " " + item3.Apellido2,
                            sexo = item3.Sexo,
                            correo = item3.Correo,
                            telefono = item3.Telefono,
                            estado = item4.Estado
                        });

            return Json(data);

        }

        [Route("Docentes")]
        public IActionResult Index() //Envia vista de Mostrar usuarios
        {

            var usuario = (object[])TempData.Peek("Usuario"); //varible de sesion
            string tipo = "0";

            if (usuario != null)
            {
                tipo = (string)usuario[4];//conversiona  entero
            }
            if (usuario != null && tipo == "1") //usuario tipo ADMINISTRADOR
            {
                return View("~/Areas/Docente/Views/Mostrar_Admin.cshtml");
            }
            else if (usuario != null && tipo == "3") //Usuario tipo INSTITUCION
            {
                return View("~/Areas/Docente/Views/Mostrar.cshtml");
            }
            else //si no existe una sesion retorna inicio de sesion 
                return View("~/Areas/Inicio de sesion/Views/login.cshtml");
        }

        public async Task<IActionResult> Agregar(string Cedula, string Nombre, string Apellido1, string Apellido2, string Sexo, string Direccion, string Correo, int Telefono, DateTime FechaNacimiento, int IdInstitucion, int Estado)
        {
            var persona = new Personas();
            var docente = new Docentes();

            var verifica = _context.Personas.Where(x => x.Cedula == Cedula && x.IdInstitucion == IdInstitucion).FirstOrDefault();

            if (verifica == null)
            {
                persona.Cedula = Cedula;
                persona.Nombre = Nombre;
                persona.Apellido1 = Apellido1;
                persona.Apellido2 = Apellido2;
                persona.Correo = Correo;
                persona.Telefono = Telefono;
                persona.Sexo = Sexo;
                persona.IdInstitucion = IdInstitucion;
                persona.Direccion = Direccion;
                persona.FechaNacimiento = FechaNacimiento;

                //Guarda en persona
                _context.Add(persona);
                await _context.SaveChangesAsync();

                docente.Estado = (ulong)Estado;
                docente.PersonasId = persona.Id;

                //Guarda en docente
                _context.Add(docente);
                await _context.SaveChangesAsync();

                return Json(docente.Id);

            }
            else
                return Json(0);

        }

    }
}
