using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Enum;
using Models.MODELS;
using Models.ViewModel;
using Services.IServices;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class UsuarioServices : IUsersService
    {
        private readonly CANCHITASGOLContext _Context;
        private readonly IMapper _mapper;

        public UsuarioServices(CANCHITASGOLContext _Context)
        {
            this._Context = _Context;
            _mapper = AutoMapperConfig.Configure();

        }

        public List<UserDTO> GetListUsers()
        {
            return _mapper.Map<List<UserDTO>>(_Context.Users.Where(e => e.Role == (int)RolsEnum.user).ToList());
        }

        public UserDTO GetUserById(int id)
        {
            return _mapper.Map<UserDTO>(_Context.Users.Where(x => x.Id == id).First());
        }

        public UserDTO CreatUser(UserViewModel user)
        {
            _Context.Users.Add(new Users()
            {
                Username = user.UserName,
                Email = user.Email
                //Role = 3
            });
            _Context.SaveChanges();

            var lastUser = _Context.Users.OrderBy(x => x.Id).Last();

            return _mapper.Map<UserDTO>(lastUser);
        }

        public void DeleteUser(int id)
        {
            _Context.Users.Remove(_Context.Users.Single(d=>d.Id == id));
            _Context.SaveChanges();
        }
        
    }
}
