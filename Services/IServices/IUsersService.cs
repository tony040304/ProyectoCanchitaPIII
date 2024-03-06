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
        void DeleteUser(int id);
        string ReserveTurn(TurnsDTO turns);
        void ChangePasword(int id, UserViewModel user);
    }
}
