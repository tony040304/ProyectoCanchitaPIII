using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class BlockedPitchDTO
    {
        public string NombreCancha { get; set; }
        public int Id { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
