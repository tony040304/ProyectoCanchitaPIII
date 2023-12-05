using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICanchitaServices
    {

        string InsertDataPitch(PitchDTO pitch);
        List<PitchTurnsDTO> GetTurnsById(int id);

    }
}
