using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.MODELS;
using Models.ViewModel;
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

        public string AddInformation(string pitchname, PitchViewModel pitch)
        {
            Pitch? pitch1 = _context.Pitch.FirstOrDefault(x=> x.Nombre == pitchname);

            if (pitch1 != null)
            {
                return "Datos ya cargados";
            }

            _context.Pitch.Add(new Pitch()
            {
                Nombre = pitchname,
                Hubicacion = pitch.Hubicacion,
                Horario = pitch.Horario,
                Canchas = pitch.Canchas,
                Telefono = pitch.Telefono
            });
            _context.SaveChanges();
            string lastPitch = _context.Pitch.OrderBy(x=>x.Nombre).Last().ToString();
            return lastPitch;
        }
        public void DeletePitchByName(string pitchname)
        {
            _context.Pitch.Remove(_context.Pitch.Single(x=> x.Nombre == pitchname));
            _context.SaveChanges();
        }

        public void UpdatePithInfo(string PitchName, PitchViewModel pitch)
        {
            Pitch? NamePitch = _context.Pitch.FirstOrDefault(x => x.Nombre == PitchName);
            NamePitch.Telefono = pitch.Telefono;
            NamePitch.Hubicacion = pitch.Hubicacion;
            NamePitch.Canchas = pitch.Canchas;
            NamePitch.Horario = pitch.Horario;

            _context.SaveChanges();
        }
        public string ReserveTurn(TurnsDTO turns)
        {
            Turns? turn = _context.Turns.FirstOrDefault(x => x.Dia == turns.Dia);

            if (turn != null || turns.Dia < DateTime.Now.Date)
            {
                return "Turno no disponible";
            }

            _context.Turns.Add(new Turns()
            {
                Dia = turns.Dia,
                NameUser = turns.NameUser,
                NamePitch = turns.NamePitch,
            });
            _context.SaveChanges();

            string lastTurn = _context.Turns.OrderBy(x => x.Id).Last().ToString();
            return lastTurn;

        }

    }
}
