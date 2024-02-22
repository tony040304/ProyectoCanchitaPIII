using Models.DTO;
using Models.MODELS;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class TurnServices : ITurnServices
    {
        public readonly CANCHITASGOLContext _context;

        public TurnServices(CANCHITASGOLContext context)
        {
            _context = context;
        }

        public List<UserTurnsDTO> GetTurnsById(string username)
        {
            var resultado = from Use in _context.Users
                            join turno in _context.Turns on Use.Username equals turno.NamePitch
                            join usuario in _context.Users on turno.NameUser equals usuario.Username
                            where turno.NamePitch == username || turno.NameUser == username
                            select new UserTurnsDTO
                            {
                                Id = turno.Id,
                                PlaceName = Use.Username,
                                UserName = usuario.Username,
                                Dia = (DateTime)turno.Dia
                            };

            return resultado.ToList();
        }

        public void DeleteTurnById(int id)
        {
            _context.Turns.Remove(_context.Turns.Single(x=>x.Id == id));
            _context.SaveChanges();
        }
    }
}