﻿using System;
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
    public class TutoresController : Controller
    {
        private readonly CentrosEscolares _context;

        public TutoresController(CentrosEscolares context)
        {
            _context = context;
        }

   
        public async Task<ActionResult> Ver (int id)// metodo ajax para recuperar datos del tutor
        {
            var data =  (from item in  _context.Tutores
                        join item2 in _context.Personas on item.PersonasId equals item2.Id
                        join item3 in _context.Oficios  on item.OficiosId equals item3.Id
                        where item.Id==id
                        select new
                        {
                            Cedula = item2.Cedula,
                            Nombre = item2.Nombre,
                            Apellido1 = item2.Apellido1,
                            Apellido2 = item2.Apellido2,
                            Sexo = item2.Sexo,
                            Fecha = item2.FechaNacimiento.Date,
                            Oficio = item3.Nombre,
                            Correo = item2.Correo,
                            telefono = item2.Telefono,
                            Direccion=item2.Direccion

                        });

            return Json(data);
        }

        public async Task<ActionResult> Datos()// metodo ajax para recuperar datos de tutores (ADMINISTRADOR)
        {
            var data = (from item in _context.Tutores.ToList()
                        join item2 in _context.Personas.ToList() on item.PersonasId equals item2.Id
                        join item3 in _context.Oficios.ToList() on item.OficiosId equals item3.Id
                        select new
                        {
                            Idtutor = item.Id,
                            Cedulat = item2.Cedula,
                            Nombret = item2.Nombre+" "+ item2.Apellido1+" "+ item2.Apellido2,
                            Sexot = item2.Sexo,
                            Oficiot = item3.Nombre,
                            Correot = item2.Correo,
                            telefonot = item2.Telefono,
                        });

            return Json(data);
        }
        public async Task<ActionResult> Datos2(int id)// metodo ajax para recuperar datos de tutores (Institucion)
        {
            var data = (from item in _context.Tutores.ToList()
                        join item2 in _context.Personas.ToList() on item.PersonasId equals item2.Id
                        join item3 in _context.Oficios.ToList() on item.OficiosId equals item3.Id
                        where item2.IdInstitucion==id
                        select new
                        {
                            Idtutor = item.Id,
                            Cedulat = item2.Cedula,
                            Nombret = item2.Nombre + " " + item2.Apellido1 + " " + item2.Apellido2,
                            Sexot = item2.Sexo,
                            Oficiot = item3.Nombre,
                            Correot = item2.Correo,
                            telefonot = item2.Telefono,
                        });

            return Json(data);
        }

        public async Task<ActionResult> Tutores(int idinstitucion)// metodo ajax para recuperar datos de tutores y llenar select
        {
            var data = (from item in _context.Tutores.ToList()
                        join item2 in _context.Personas.ToList() on item.PersonasId equals item2.Id
                        where item2.IdInstitucion==idinstitucion
                        select new
                        {
                            Idtutor = item.Id,
                            Nombret = item2.Nombre + " " + item2.Apellido1 + " " + item2.Apellido2
                        });

            return Json(data);
        }


        public async Task<ActionResult> AgregarTutor(string Cedula, string Nombre, string Apellido1, string Apellido2, string Sexo, string Direccion, string Correo, int Telefono, DateTime FechaNacimiento, int IdInstitucion, int OficiosId)
        {
            var persona = new Personas();
            var tutor = new Tutores();

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


                tutor.OficiosId = OficiosId;
                tutor.PersonasId = persona.Id;

                //Guarda en tutor
                _context.Add(tutor);
                await _context.SaveChangesAsync();

                return Json(tutor.Id);

            }
            else
                return Json(0);
        }

    }
}
