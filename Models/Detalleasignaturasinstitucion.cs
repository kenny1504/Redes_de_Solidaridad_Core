﻿using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Detalleasignaturasinstitucion
    {
        public int Id { get; set; }
        public int IdInstitucion { get; set; }
        public int IdAsignatura { get; set; }
    }
}
