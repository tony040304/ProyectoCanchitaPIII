using Models.DTO;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IUsersService
    {
        List<PitchTurnsDTO> GetListPitch(DateTime date);
        void DeleteUser(string username);
        string ReserveTurn(TurnsDTO turns);
    }
}
