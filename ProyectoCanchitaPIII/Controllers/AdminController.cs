using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.ViewModel;
using Services.IServices;
using Services.Service;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "1")]
    public class AdminController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ICanchitaServices _canchitaServices;
        private readonly ITurnServices _turnServices;
        private readonly IAdminServices _adminServices;

        public AdminController(ICanchitaServices services, IUsersService Services1, ITurnServices turnServices, IAdminServices adminServices)
        {
            _canchitaServices = services;
            _usersService = Services1;
            _turnServices = turnServices;
            _adminServices = adminServices;
        }

        [HttpDelete("DeletePitch")]
        public ActionResult DeletePitchByName([FromQuery] int id)
        {
            try
            {
                _canchitaServices.DeletePitchByName(id);
                return Ok("Cancha borrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        
        [HttpPut("UnlockPitch")]
        public ActionResult UnlockPitch(int Id, BlockViewModel block)
        {
            try
            {
                _adminServices.UnlockPitch(Id, block);
                return Ok("Actualizado correctamente");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBlockedPitchList")]
        public ActionResult<PitchDTO> GetBlockedPitchList()
        {
            try
            {
                var response = _adminServices.GetBlockedPitchList();
                if (response.Count == 0)
                {
                    return NotFound("no hay canchas bloqueadas");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPitchList")]
        public ActionResult<PitchDTO> GetPitchList()
        {
            try
            {
                var response = _adminServices.GetPitchList();
                if (response.Count == 0)
                {
                    return NotFound("No hay canchas registradas");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUsersList")]
        public ActionResult<UserDTO> GetUserList()
        {
            try
            {
                var response = _adminServices.GetUserList();
                if (response.Count == 0)
                {
                    return NotFound("No hay usuarios registradas");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetTurnList/{pitchId}")]
        public ActionResult<PitchDTO> GetTurnList(int pitchId)
        {
            try
            {
                var response = _adminServices.GetTurnList(pitchId);
                if (response.Count == 0)
                {
                    return NotFound("No hay turnos registrados");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetTurnListUser/{userId}")]
        public ActionResult<PitchDTO> GetTurnListUser(int userId)
        {
            try
            {
                var response = _adminServices.GetTurnListUser(userId);
                if (response.Count == 0)
                {
                    return NotFound("No hay turnos registrados");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}