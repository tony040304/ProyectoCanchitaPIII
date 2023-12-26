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

        public List<PitchDTO> GetListPitch()
        {
            return _Context.Users
            .Where(u => u.Role == 2)
            .Select(s => new PitchDTO() { Id = s.Id, Email = s.Email, Username = s.Username })
            .ToList();
        }

        
        public void DeleteUser(int id)
        {
            _Context.Users.Remove(_Context.Users.Single(d => d.Id == id));
            _Context.SaveChanges();
        }

        public string ReserveTurn(TurnsDTO turns)
        {
            Turns? turn = _Context.Turns.FirstOrDefault(x => x.Dias == turns.Dias);

            if (turn != null || turns.Dias < DateTime.Now.Date)
            {
                return "Turno no disponible";
            }

            _Context.Turns.Add(new Turns()
            {
                Dias = turns.Dias,
                IdUsers = turns.IdUsers,
                IdPitch = turns.IdPitch,
            });
            _Context.SaveChanges();

            string lastTurn = _Context.Turns.OrderBy(x=>x.IdTurns).Last().ToString();
            return lastTurn;

        }


    }
}
