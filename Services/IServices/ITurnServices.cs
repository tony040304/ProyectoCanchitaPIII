using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ITurnServices
    {
        List<UserTurnsDTO> GetTurnsById(string username);
        void DeleteTurnById(int id);
    }
}
