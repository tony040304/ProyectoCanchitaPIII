using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CanchitaServices : ICanchitaServices
    {
        private readonly CANCHITASGOLContext _context;
        private readonly IMapper _mapper;

        public CanchitaServices(CANCHITASGOLContext _context)
        {
            this._context = _context;
            _mapper = AutoMapperConfig.Configure();
        }
        
        public string InsertDataPitch(PitchDTO pitch)
        {
            Pitch? pitch1 = _context.Pitch.FirstOrDefault(x=> x.PlaceName == pitch.PlaceName);

            if (pitch1 != null)
            {
                return "Cancha existente";
            }

            _context.Pitch.Add(new Pitch(){
                Owner = pitch.Owner,
                PlaceName = pitch.PlaceName,
                IdUsuario = pitch.IdUsuario
            });
            _context.SaveChanges();

            string lastPitch = _context.Pitch.OrderBy(x=> x.IdPitch).Last().ToString();
            return lastPitch;
        }

        public List<PitchTurnsDTO> GetTurnsById(int id)
        {
            var resultado = from pitch in _context.Pitch
                            join turno in _context.Turns on pitch.IdPitch equals turno.IdPitch
                            join usuario in _context.Users on turno.IdUsers equals usuario.Id
                            where pitch.IdPitch == id
                            select new PitchTurnsDTO
                            {
                                PlaceName = pitch.PlaceName,
                                UserName = usuario.Username,
                                Dia = turno.Dia
                            };

            return resultado.ToList();
        }

    }
}
