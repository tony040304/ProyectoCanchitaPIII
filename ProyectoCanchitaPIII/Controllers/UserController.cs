using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.ViewModel;
using Services.IServices;
using Microsoft.AspNetCore.Http;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _logger;
        public UserController(IUsersService usersService, IConfiguration logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet("GetListOfUsers")]
        public ActionResult<List<UserDTO>> GetListOwners()
        {
            try
            {
                var response = _usersService.GetListOwners();
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

        [HttpGet("GetOwnerById/{id}")]
        public ActionResult<UserDTO> GetOwnerById(int id)
        {
            try
            {
                var response = _usersService.GetOwnerById(id);
                if (response == null)
                {
                    NotFound($"No hay usuario con ese id {id}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
    }
}
