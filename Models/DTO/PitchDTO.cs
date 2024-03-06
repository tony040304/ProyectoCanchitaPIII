﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PitchDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Horario { get; set; }
        public string Ubicacion { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
