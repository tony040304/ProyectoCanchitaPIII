using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.ViewModel;
using Services.IServices;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticate _services;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IAuthenticate services, ILogger<AuthenticateController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] AuthenticateViewModel User)
        {
            string response = string.Empty;

            try
            {
                response = _services.Login(User);
                if (string.IsNullOrEmpty(response))
                {
                    return NotFound("username o contraseña incorrecta");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Login", ex);
                return BadRequest(ex);
            }
            return Ok(response);
        }
        [HttpPost("Sing up")]
        public ActionResult<string> CreatUser([FromBody] UserDTO User)
        {
            string response = string.Empty;
            try
            {
                response = _services.CreatUser(User);
                if (response == "Ingrese un usuario" || response == "Usuario existente")
                    return BadRequest(response);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("CrearUsuario", ex);
                return BadRequest($"{ex.Message}");
            }
            return Ok(response);
    }
    }

    
}
