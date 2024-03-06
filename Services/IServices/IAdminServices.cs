using Models.DTO;
using Models.MODELS;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAdminServices
    {
        //string BlockPitch(PitchDTO blockedPitch);
        void UnlockPitch(int Id, BlockViewModel block);
        List<PitchDTO> GetBlockedPitchList();
        List<UserDTO> GetUserList();
        List<PitchDTO> GetPitchList();
        List<TurnsDTO> GetTurnList(int pitchId);
        List<TurnsDTO> GetTurnListUser(int userId);
    }
}
