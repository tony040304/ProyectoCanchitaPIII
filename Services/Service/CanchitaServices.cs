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
        public void DeletePitchByName(int id)
        {
            _context.Pitch.Remove(_context.Pitch.Single(x=> x.Id == id));
            _context.SaveChanges();
        }

        public void UpdatePithInfo(int id, PitchViewModel pitch)
        {
            Pitch? NamePitch = _context.Pitch.FirstOrDefault(x => x.Id == id);
            NamePitch.Nombre = pitch.Nombre;
            NamePitch.Ubicacion = pitch.Ubicacion;
            NamePitch.Email = pitch.Email;
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
            Pitch pitch = _context.Pitch.FirstOrDefault(p => p.Id == turns.IdPitch && (bool)p.IsBlocked);
            if (pitch != null)
            {
                return "La cancha está bloqueada y no se puede reservar en este momento";
            }

            _context.Turns.Add(new Turns()
            {
                Dia = turns.Dia,
                IdPitch = turns.IdPitch,
                Descripcion = turns.Descripcion
            });
            _context.SaveChanges();

            string lastTurn = _context.Turns.OrderBy(x => x.Id).Last().ToString();
            return lastTurn;

        }

    }
}
