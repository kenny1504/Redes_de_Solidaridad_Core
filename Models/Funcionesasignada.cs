using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Funcionesasignada
    {
        public int IdFuncionAcceso { get; set; }
        public int Id { get; set; }
        public DateTime FechaDeVencimiento { get; set; }
        public int IdUsuarios { get; set; }
    }
}
