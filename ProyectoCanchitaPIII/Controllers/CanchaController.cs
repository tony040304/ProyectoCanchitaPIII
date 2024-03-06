using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.MODELS;
using Models.ViewModel;
using Services.IServices;
using Services.Service;
using System.Diagnostics.Contracts;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "2")]
    public class CanchaController : ControllerBase

    {
        private readonly ICanchitaServices _service;
        private readonly ITurnServices _turnServices;
        public CanchaController (ICanchitaServices Services, ITurnServices turn)
        {
            _service = Services;
            _turnServices = turn;
        }


        
        [HttpPost("ReservarTurno")]
        public ActionResult<string> ReserveTurn([FromBody] TurnsDTO turns)
        {
            string response = string.Empty;
            try
            {
                response = _service.ReserveTurn(turns);
                if (response == "Turno no disponible")
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Turno resrvado");
        }

        [HttpGet("GetListTurns/{id}")]
        public ActionResult<List<UserTurnsDTO>> GetTurnsById(int id)
        {
            try
            {
                var response = _turnServices.GetTurnsById(id);
                if (response == null)
                {
                    NotFound("No hay reservas");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletePitch")]
        public ActionResult DeletePitchByName([FromQuery] int id)
        {
            try
            {
                _service.DeletePitchByName(id);
                return Ok("Cancha borrada");
            }
            catch
            {
                return BadRequest("Tiene turnos reservados, no se puede borrar esta cancha.");
            }
        }
        [HttpDelete("Deleteturn")]
        public ActionResult DeleteTurnById([FromQuery] int id)
        {
            try
            {
                _turnServices.DeleteTurnById(id);
                return Ok("Turno borrado");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdatePitchInfo/{id}")]
        public ActionResult UpdatePithInfo(int id, PitchViewModel pitch)
        {
            try
            {
                _service.UpdatePithInfo(id, pitch);
                return Ok("Datos actualizados");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
