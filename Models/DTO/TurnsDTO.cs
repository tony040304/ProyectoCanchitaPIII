using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class TurnsDTO
    {
        public int Id { get; set; }
        public DateTime? Dia { get; set; }
        public int? IdPitch { get; set; }
        public int? IdUser { get; set; }
        public string Descripcion { get; set; }
    }
}
