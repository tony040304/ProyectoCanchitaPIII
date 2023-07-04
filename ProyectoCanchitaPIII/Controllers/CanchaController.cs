using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.MODELS;
using Services.IServices;
using Services.Service;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CanchaController : ControllerBase

    {
        private readonly ICanchitaGolServices _service;

        public CanchaController (ICanchitaGolServices Services)
        {
            _service = Services;
        }

        [HttpGet("GetListPitch")]

        public ActionResult<List<PitchDTO>> GetListPitch()
        {
            var response = _service.GetListPitch();
            return Ok(response);
        }

        [HttpGet("GetListOfUsers")]
        public ActionResult<List<UserDTO>> GetListOfUsers()
        {
            var response = _service.GetListOfUsers();

            return Ok(response);
        }

        //[HttpGet("GetUserByName/{id}")]
        //public ActionResult<UserDTO> GetUserByName(int id)
        //{
        //    var response = _service.GetUserByName(id);

        //    return Ok(response);
        //}

    }
}
