using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.MODELS;
using Services.IServices;
using Services.Service;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("ProyectoCanchitaPIII/[Controller]")]
    [ProyectoCanchitaPIIIControllers]
    public class CanchaController : ControllerBase

    {
        private readonly ICanchitaGolServices _service;

        public CanchaController (ICanchitaGolServices Services)
        {
            _service = Services;
        }

        [HttpGet("GetListadoCancha")]

        public ActionResult<List<CanchaDTO>> GetListadoCancha()
        {
            var response = _service.GetListPitch();
            return Ok(response);
        }

    }
}
