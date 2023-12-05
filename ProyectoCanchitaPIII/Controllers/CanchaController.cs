using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.MODELS;
using Services.IServices;
using Services.Service;
using System.Diagnostics.Contracts;

namespace ProyectoCanchitaPIII.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CanchaController : ControllerBase

    {
        private readonly ICanchitaServices _service;

        public CanchaController (ICanchitaServices Services)
        {
            _service = Services;
        }
        [HttpPost("DataPitch")]
        public ActionResult<string> InsertDataPitch(PitchDTO pitch)
        {
            string response = string.Empty;
            try
            {
                response = _service.InsertDataPitch(pitch);
                if (response == "Cancha existente")
                    return BadRequest(response);

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }


        [HttpGet("GetListTurns")]
        public ActionResult<List<PitchTurnsDTO>> GetTurnsById(int id)
        {
            try
            {
                var response = _service.GetTurnsById(id);
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


        

    }
}
