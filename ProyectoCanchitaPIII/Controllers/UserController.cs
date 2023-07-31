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
        private readonly ILogger<UserController> _logger;
        public UserController(IUsersService usersService, ILogger<UserController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet("GetListOfUsers")]
        public ActionResult<List<UserDTO>> GetListUsers()
        {
            try
            {
                var response = _usersService.GetListUsers();
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

        [HttpGet("GetuserById/{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            try
            {
                var response = _usersService.GetUserById(id);
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

        [HttpPost("CreatUser")]
        public ActionResult<UserDTO> CreatUser([FromBody] UserViewModel user)
        {
            try
            {
                var response = _usersService.CreatUser(user);

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/User/GetuserById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.Id}";
                
                return Created(locationUrl, response);
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
    }
}
