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
    }
}