using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.MODELS;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CanchitaGolServices : ICanchitaGolServices
    {
        private readonly CANCHITASGOLContext _context;

        public CanchitaGolServices(CANCHITASGOLContext _context)
        {
            this._context = _context;
        }
        public List<PitchDTO> GetListPitch()
        {
            var cancha = _context.Pitch.ToList();
            var canchaResponse = new List<PitchDTO>();

            foreach(var c in cancha)
            {
                canchaResponse.Add(new PitchDTO()
                {
                    IdPitch = c.IdPitch,
                    Owner = c.Owner
                });
            }
            return canchaResponse;
        }
        public List<UserDTO> GetListOfUsers()
        {
            var users = _context.Users.ToList();
            var usersResponse = new List<UserDTO>();

            foreach (var user in users)
            {
                usersResponse.Add(new UserDTO()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Userpassword = user.Userpassword,
                    Email = user.Email,
                    Role = user.Role
                });
            }
            return usersResponse;
        }

        //public UserDTO GetUserByName(int id)
        //{
        //    UserDTO user = usersResponse.Where(x => x.Id == id).FirstOrDefault();
        //    return user;
        //}

    }
}
