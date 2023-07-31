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
        string CreatUser(UserViewModel User);
        string Login(AuthenticateViewModel User);

    }
}
