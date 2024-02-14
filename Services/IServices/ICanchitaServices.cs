using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICanchitaServices
    {
        void DeletePitchById(int id);

        string AddInformation(PitchDTO pitch);
    }
}
