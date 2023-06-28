using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PitchDTO
    {
        public int IdPitch { get; set; }
        public string Owner { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
    }
}
