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
        void DeletePitchByName(string pitchname);
        void UpdatePithInfo(string PitchName, PitchViewModel pitch);
        string AddInformation(string pitchname, PitchViewModel pitch);
    }
}
