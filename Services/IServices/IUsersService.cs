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
        List<UserDTO> GetListUsers();
        UserDTO GetUserById(int id);
        UserDTO CreatUser(UserViewModel user);
        void DeleteUser(int id);
    }
}
