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
    //[Authorize(Roles = "3")]
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

        [HttpGet("GetListOfPitchs")]
        public ActionResult<List<PitchDTO>> GetListPitch()
        {
            try
            {
                var response = _usersService.GetListPitch();
                if (response == null)
                {
                    NotFound("No hay usuario");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _usersService.DeleteUser(id);
                return Ok();
            }
            catch(Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ReservarTurno")]
        public ActionResult<string> ReserveTurn(TurnsDTO turns)
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
            return Ok(response);
        }
        [HttpDelete("Deleteturn/{id}")]
        public ActionResult DeleteTurnById([FromRoute] int id)
        {
            try
            {
                _turnServices.DeleteTurnById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
