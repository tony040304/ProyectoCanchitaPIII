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

        public List<UserTurnsDTO> GetTurnsById(int id)
        {
            var resultado = from Use in _context.Users
                            join turno in _context.Turns on Use.Id equals turno.IdPitch
                            join usuario in _context.Users on turno.IdUsers equals usuario.Id
                            where turno.IdPitch == id || turno.IdUsers == id
                            select new UserTurnsDTO
                            {
                                Id = turno.IdTurns,
                                PlaceName = Use.Username,
                                UserName = usuario.Username,
                                Dia = (DateTime)turno.Dias
                            };

            return resultado.ToList();
        }

        public void DeleteTurnById(int id)
        {
            _context.Turns.Remove(_context.Turns.Single(x=>x.IdTurns == id));
            _context.SaveChanges();
        }
    }
}
