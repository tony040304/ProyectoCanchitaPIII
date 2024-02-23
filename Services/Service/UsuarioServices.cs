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
                         join t in _Context.Turns.Where(t => t.Dia == date) on p.Nombre equals t.NamePitch into joinedTurns
                         from t in joinedTurns.DefaultIfEmpty()
                         where t.NameUser == null &&
                         !_Context.BlockedPitch.Any(bp => bp.NombreCancha == p.Nombre)
                         select new PitchTurnsDTO
                         {
                             Nombre = p.Nombre,
                             Horario = p.Horario,
                             Hubicacion = p.Hubicacion,
                             Canchas = p.Canchas,
                             Telefono = p.Telefono,
                         };


            return result.ToList();
        }


        public void DeleteUser(string username)
        {
            _Context.Users.Remove(_Context.Users.Single(d => d.Username == username));
            _Context.SaveChanges();
        }
        public string ReserveTurn(TurnsDTO turns)
        {
            Turns? turn = _Context.Turns.FirstOrDefault(x => x.Dia == turns.Dia);

            if (turn != null || turns.Dia < DateTime.Now.Date)
            {
                return "Turno no disponible";
            }

            _Context.Turns.Add(new Turns()
            {
                Dia = turns.Dia,
                NameUser = turns.NameUser,
                NamePitch = turns.NamePitch,
            });
            _Context.SaveChanges();

            string lastTurn = _Context.Turns.OrderBy(x=>x.Id).Last().ToString();
            return lastTurn;

        }
        public void ChangePasword(string username, UserViewModel user)
        {
            var UserModify = _Context.Users.Where(x=> x.Username == username).First();
            UserModify.Userpassword = user.Userpassword;
            _Context.SaveChanges();
        }


    }
}