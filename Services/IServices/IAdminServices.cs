using Models.DTO;
using Models.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAdminServices
    {
        string BlockPitch(BlockedPitchDTO blockedPitch);
        void UnlockPitch(int blockedPitchId);
        List<BlockedPitchDTO> GetBlockedPitchList();
        List<UserDTO> GetUserList();
        List<PitchDTO> GetPitchList();
        List<TurnsDTO> GetTurnList();
    }
}
