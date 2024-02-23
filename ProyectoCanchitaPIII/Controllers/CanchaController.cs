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


        [HttpPost("AddInformation/{pitchname}")]
        public ActionResult<string> AddInformation(string pitchname, [FromBody] PitchViewModel pitch)
        {
            string response = string.Empty;
            try
            {
                response = _service.AddInformation(pitchname ,pitch);
                if (response == "Datos ya cargados")
                {
                    return BadRequest(response);
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Datos cargados correctamente");
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

        [HttpGet("GetListTurns/{username}")]
        public ActionResult<List<UserTurnsDTO>> GetTurnsById(string username)
        {
            try
            {
                var response = _turnServices.GetTurnsById(username);
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
        public ActionResult DeletePitchByName([FromQuery] string pitchname)
        {
            try
            {
                _service.DeletePitchByName(pitchname);
                return Ok("Cancha borrada");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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
        [HttpPut("UpdatePitchInfo/{PitchName}")]
        public ActionResult UpdatePithInfo(string PitchName, PitchViewModel pitch)
        {
            try
            {
                _service.UpdatePithInfo(PitchName, pitch);
                return Ok("Datos actualizados");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
