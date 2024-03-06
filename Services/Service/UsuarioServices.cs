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

        public List<PitchTurnsDTO> GetListPitch(DateTime date)
        {
            var result = from p in _Context.Pitch
                         join t in _Context.Turns.Where(turn => turn.Dia == date)
                         on p.Id equals t.IdPitch into pitchTurns
                         from pt in pitchTurns.DefaultIfEmpty()
                         where pt == null && (p.IsBlocked == false || p.IsBlocked == null)
                         select new PitchTurnsDTO
                         {
                             Id = p.Id,
                             Nombre = p.Nombre,
                             Email = p.Email,
                             Horario = p.Horario,
                             Ubicacion = p.Ubicacion
                         };

            return result.ToList();
        }



        public void DeleteUser(int id)
        {
            _Context.Users.Remove(_Context.Users.Single(d => d.Id == id));
            _Context.SaveChanges();
        }
        public string ReserveTurn(TurnsDTO turns)
        {
            Turns? turn = _Context.Turns.FirstOrDefault(x => x.Dia == turns.Dia);
            if (turn != null || turns.Dia < DateTime.Now.Date)
            {
                return "Turno no disponible";
            }
            Pitch pitch = _Context.Pitch.FirstOrDefault(p => p.Id == turns.IdPitch && (bool)p.IsBlocked);
            if (pitch != null)
            {
                return "La cancha está bloqueada y no se puede reservar en este momento";
            }


            _Context.Turns.Add(new Turns()
            {
                Dia = turns.Dia,
                IdUser = turns.IdUser,
                IdPitch = turns.IdPitch,
                Descripcion = turns.Descripcion
            });
            _Context.SaveChanges();

            string lastTurn = _Context.Turns.OrderBy(x=>x.Id).Last().ToString();
            return lastTurn;

        }
        public void ChangePasword(int id, UserViewModel user)
        {
            var UserModify = _Context.Users.Where(x=> x.Id == id).First();
            UserModify.Userpassword = user.Userpassword;
            _Context.SaveChanges();
        }


    }
}