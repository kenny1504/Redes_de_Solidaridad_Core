using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Personas
    {
        public Personas()
        {
            Docentes = new HashSet<Docentes>();
            Estudiantes = new HashSet<Estudiantes>();
            Tutores = new HashSet<Tutores>();
        }

        public uint Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public uint Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<Docentes> Docentes { get; set; }
        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
        public virtual ICollection<Tutores> Tutores { get; set; }
    }
}
