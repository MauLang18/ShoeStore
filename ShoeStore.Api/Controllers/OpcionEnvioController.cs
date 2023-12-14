using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.OpcionEnvio.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OpcionEnvioController : ControllerBase
    {
        private readonly IOpcionEnvioApplication _opcionEnvioApplication;

        public OpcionEnvioController(IOpcionEnvioApplication opcionEnvioApplication)
        {
            _opcionEnvioApplication = opcionEnvioApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListOpcionesEnvios([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _opcionEnvioApplication.ListOpcionesEnvios(filters);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectOpcionesEnvios()
        {
            var response = await _opcionEnvioApplication.ListSelectOpcionesEnvios();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> OpcionEnvioById(int id)
        {
            var response = await _opcionEnvioApplication.OpcionEnvioById(id);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterOpcionEnvio([FromBody] OpcionEnvioRequestDto requestDto)
        {
            var response = await _opcionEnvioApplication.RegisterOpcionEnvio(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditOpcionEnvio(int id, [FromBody] OpcionEnvioRequestDto requestDto)
        {
            var response = await _opcionEnvioApplication.EditOpcionEnvio(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveOpcionEnvio(int id)
        {
            var response = await _opcionEnvioApplication.RemoveOpcionEnvio(id);

            return Ok(response);
        }
    }
}
