using AutoMapper;
using Models.DTO;
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
    public class AdminServices : IAdminServices
    {
        private readonly CANCHITASGOLContext _context;
        private readonly IMapper _mapper;

        public AdminServices(CANCHITASGOLContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public void UnlockPitch(int Id, BlockViewModel block)
        {
            Pitch? NamePitch = _context.Pitch.FirstOrDefault(x => x.Id == Id);
            NamePitch.IsBlocked = block.isBlocked;

            _context.SaveChanges();
        }
        public List<PitchDTO> GetBlockedPitchList()
        {
            return _mapper.Map<List<PitchDTO>>(_context.Pitch.Where(x=>x.IsBlocked == true).ToList());
        }

        public List<UserDTO> GetUserList()
        {
            return _mapper.Map<List<UserDTO>>(_context.Users.Where(x=>x.Role == "0").ToList());
        }

        public List<PitchDTO> GetPitchList()
        {
            return _mapper.Map<List<PitchDTO>>(_context.Pitch.ToList());
        }
        public List<TurnsDTO> GetTurnList(int pitchId)
        {
            return _mapper.Map<List<TurnsDTO>>(_context.Turns.Where(x=>x.IdPitch == pitchId).ToList());
        }

        public List<TurnsDTO> GetTurnListUser(int userId)
        {
            return _mapper.Map<List<TurnsDTO>>(_context.Turns.Where(x => x.IdUser == userId).ToList());
        }
    }
}