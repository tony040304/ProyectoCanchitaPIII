using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.ViewModel;
using Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "0")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _logger;
        private readonly ITurnServices _turnServices;
        public UserController(IUsersService usersService, IConfiguration logger, ITurnServices turnServices)
        {
            _usersService = usersService;
            _logger = logger;
            _turnServices = turnServices;
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

        [HttpGet("GetListOfavailablePitchs/{date}")]
        public ActionResult<List<PitchDTO>> GetListPitch(DateTime date)
        {
            try
            {
                var response = _usersService.GetListPitch(date);
                if (response == null)
                {
                    NotFound("No hay canchas disponibles ese dia");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser([FromQuery] int id)
        {
            try
            {
                _usersService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest($"Tiene turno reservado, no puede puede borrar este usuario");
            }
        }

        [HttpPost("ReservarTurno")]
        public ActionResult<string> ReserveTurn([FromBody] TurnsDTO turns)
        {
            string response = string.Empty;
            try
            {
                response = _usersService.ReserveTurn(turns);
                if (response == "Turno no disponible" || response == "Cancha bloqueada" || response == "La cancha está bloqueada y no se puede reservar en este momento")
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Turno reservado");
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
        [HttpPut("ChangePasswords/{id}")]
        public ActionResult ChangePasword(int id,[FromBody] UserViewModel user)
        {
            try
            {
                _usersService.ChangePasword(id, user);
                return Ok("Contraseña cambiada");   
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}