using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UserTurnsDTO
    {
        public int Id { get; set; }
        public DateTime Dia { get; set; }
        public string PlaceName { get; set; }
        public string UserName { get; set; }
        public string Descripcion { get; set; }

    }
}
