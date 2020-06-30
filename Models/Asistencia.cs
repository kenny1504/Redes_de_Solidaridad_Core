using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Asistencia
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public ulong Estado { get; set; }
        public int IdMatricula { get; set; }
    }
    public class ListaAsistencia
    {
        public int IdMatricula { get; set; }
        public string Nombre { get; set; }
        public string CodigoEstudinte { get; set; }
    }
    public class BuscarAsistencia
    {
        public int IdInstitucion { get; set; }
        public string Cedula { get; set; }
    }
}
