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
        public ActionResult DeleteUser([FromQuery] string username)
        {
            try
            {
                _usersService.DeleteUser(username);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ReservarTurno")]
        public ActionResult<string> ReserveTurn([FromBody] TurnsDTO turns)
        {
            string response = string.Empty;
            try
            {
                response = _usersService.ReserveTurn(turns);
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
        [HttpPut("ChangePasswords/{username}")]
        public ActionResult ChangePasword(string username,[FromBody] UserViewModel user)
        {
            try
            {
                _usersService.ChangePasword(username, user);
                return Ok("Contraseña cambiada");   
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}