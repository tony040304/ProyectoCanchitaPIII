using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "2")]
    public class CanchaController : ControllerBase

    {
        private readonly ICanchitaServices _service;
        private readonly ITurnServices _turnServices;
        public CanchaController (ICanchitaServices Services, ITurnServices turn)
        {
            _service = Services;
            _turnServices = turn;
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

        [HttpDelete("DeletePitch/{id}")]
        public ActionResult DeletePitchById(int id)
        {
            try
            {
                _service.DeletePitchById(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Deleteturn/{id}")]
        public ActionResult DeleteTurnById(int id)
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
