using Models.DTO;
using Models.ViewModel;
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
        void DeletePitchByName(int id);
        void UpdatePithInfo(int id, PitchViewModel pitch);
        string ReserveTurn(TurnsDTO turns);
    }
}
