using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.IServices;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("GetListOfUsers")]
        public ActionResult<List<UserDTO>> GetListOfUsers()
        {
            var response = _usersService.GetListOfUsers();

            return Ok(response);
        }

        [HttpGet("GetUserByName/{id}")]
        public ActionResult<UserDTO> GetUserByName(int id)
        {
            var response = _usersService.GetUserByName(id);

            return Ok(response);
        }
    }
}
