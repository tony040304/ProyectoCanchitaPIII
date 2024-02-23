using AutoMapper;
using Models.DTO;
using Models.MODELS;
using Services.IServices;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AdminServices : IAdminServices
    {
        private readonly CANCHITASGOLContext _context;
        private readonly IMapper _mapper;

        public AdminServices(CANCHITASGOLContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }


        public string BlockPitch(BlockedPitchDTO blockedPitch)
        {
            BlockedPitch? blocked = _context.BlockedPitch.FirstOrDefault(x=> x.Id == blockedPitch.Id);

            if (blocked != null)
            {
                return "Cancha bloqueada";
            }

            _context.BlockedPitch.Add(new BlockedPitch()
            {
                Id = blockedPitch.Id,
                NombreCancha = blockedPitch.NombreCancha,
                IsBlocked = true,
            });
            _context.SaveChanges();

            string lastBlocked = _context.BlockedPitch.OrderBy(x=>x.Id).Last().ToString();
            return lastBlocked;
        }

        public void UnlockPitch(int blockedPitchId)
        {
            _context.BlockedPitch.Remove(_context.BlockedPitch.Single(x => x.Id == blockedPitchId));
            _context.SaveChanges();
        }
        public List<BlockedPitchDTO> GetBlockedPitchList()
        {
            return _context.BlockedPitch.ToList().Select(x => new BlockedPitchDTO() { Id = x.Id, IsBlocked = x.IsBlocked, NombreCancha = x.NombreCancha }).ToList();
        }

        public List<UserDTO> GetUserList()
        {
            return _context.Users.ToList().Where(x=>x.Role == 0).Select(x => new UserDTO() { Username = x.Username, Email = x.Email, Userpassword = x.Userpassword, Id = x.Id, Role = (int)x.Role }).ToList();
        }

        public List<PitchDTO> GetPitchList()
        {
            return _context.Pitch.ToList().Select(x => new PitchDTO() { Canchas = x.Canchas, Horario = x.Horario, Hubicacion = x.Hubicacion, Nombre = x.Nombre , Telefono = x.Telefono }).ToList();
        }
        public List<TurnsDTO> GetTurnList()
        {
            return _context.Turns.ToList().Select(x => new TurnsDTO() { Id = x.Id, Dia = x.Dia, NamePitch = x.NamePitch, NameUser = x.NameUser }).ToList();
        }
    }
}