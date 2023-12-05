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

        public List<UserDTO> GetListOwners()
        {
            return _mapper.Map<List<UserDTO>>(_Context.Users.Where(e => e.Role == (int)RolsEnum.owner).ToList());
        }

        public UserDTO GetOwnerById(int id)
        {
            return _mapper.Map<UserDTO>(_Context.Users.Where(x => x.Id == id).First());
        }

        public void DeleteUser(int id)
        {
            _Context.Users.Remove(_Context.Users.Single(d=>d.Id == id));
            _Context.SaveChanges();
        }
        
    }
}
