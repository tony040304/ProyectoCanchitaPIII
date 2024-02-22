using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.IServices;
using Services.Service;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "1")]
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

        [HttpDelete("DeletePitch/{id}")]
        public ActionResult DeletePitchById(int id)
        {
            try
            {
                _canchitaServices.DeletePitchById(id);
                return Ok("Cancha borrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteUser/{username}")]
        public ActionResult DeleteUser(string username)
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
[11:44]
[HttpDelete("Deleteturn/{id}")]
        public ActionResult DeleteTurnById(int id)
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
        [HttpPost("BlockPitch")]
        public ActionResult<string> BlockPitch(BlockedPitchDTO blockedPitch)
        {
            string response = string.Empty;
            try
            {
                response = _adminServices.BlockPitch(blockedPitch);
                if(response == "Cancha bloqueada")
                {
                    return BadRequest();
                }
                return Ok("Cancha bloqueada");
            }
            catch(Exception ex) 
            {
                return BadRequest($"{ex.Message}");
            }

        }
        [HttpDelete("UnlockPitch/{blockedPitchId}")]
        public ActionResult UnlockPitch(int blockedPitchId)
        {
            try
            {
                _adminServices.UnlockPitch(blockedPitchId);
                return Ok("Borrado correctamente");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBlockedPitchList")]
        public ActionResult<BlockedPitchDTO> GetBlockedPitchList()
        {
            try
            {
                var response = _adminServices.GetBlockedPitchList();
                if (response.Count == 0)
                {
                    return NotFound("No hay canchas bloqueadas");
                }
                return Ok(response);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}