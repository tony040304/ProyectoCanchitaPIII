using Models.DTO;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthenticate
    {
        string CreateUser(UserDTO User);
        string CreatePitch(PitchDTO Pitch);
        string Login(AuthenticateViewModel User);

    }
}
