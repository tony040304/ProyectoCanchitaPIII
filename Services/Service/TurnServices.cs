using Microsoft.EntityFrameworkCore;
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
            var resultado = from t in _context.Turns
                        join p in _context.Pitch on t.IdPitch equals p.Id into pitchGroup
                        from p in pitchGroup.DefaultIfEmpty()
                        join u in _context.Users on t.IdUser equals u.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                        where t.IdPitch == id || t.IdUser == id
                        select new UserTurnsDTO
                        {
                            Id = t.Id,
                            Dia = (DateTime)t.Dia,
                            Descripcion = t.Descripcion,
                            PlaceName = p != null ? p.Nombre : null,
                            UserName = u != null ? u.Username : null
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