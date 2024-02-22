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
using System.Security.Claims;
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

        public string AddInformation(PitchDTO pitch)
        {
            Pitch? pitch1 = _context.Pitch.FirstOrDefault(x=> x.Nombre == pitch.Nombre);

            if (pitch1 != null)
            {
                return "Datos ya cargados";
            }

            _context.Pitch.Add(new Pitch()
            {
                Nombre = pitch.Nombre,
                Hubicacion = pitch.Hubicacion,
                Horario = pitch.Horario,
                Canchas = pitch.Canchas,
                Telefono = pitch.Telefono
            });
            _context.SaveChanges();
            string lastPitch = _context.Pitch.OrderBy(x=>x.Nombre).Last().ToString();
            return lastPitch;
        }
        public void DeletePitchById(int id)
        {
            _context.Users.Remove(_context.Users.Single(x=> x.Id == id));
            _context.SaveChanges();
        }

    }
}
