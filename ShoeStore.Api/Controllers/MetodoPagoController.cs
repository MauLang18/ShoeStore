using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.MetodoPago.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoApplication _metodoPagoApplication;

        public MetodoPagoController(IMetodoPagoApplication metodoPagoApplication)
        {
            _metodoPagoApplication = metodoPagoApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListMetodosPagos([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _metodoPagoApplication.ListMetodosPagos(filters);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectMetodosPagos()
        {
            var response = await _metodoPagoApplication.ListSelectMetodosPagos();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> MetodoPagoById(int id)
        {
            var response = await _metodoPagoApplication.MetodoPagoById(id);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterMetodoPago([FromBody] MetodoPagoRequestDto requestDto)
        {
            var response = await _metodoPagoApplication.RegisterMetodoPago(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditMetodoPago(int id, [FromBody] MetodoPagoRequestDto requestDto)
        {
            var response = await _metodoPagoApplication.EditMetodoPago(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveMetodoPago(int id)
        {
            var response = await _metodoPagoApplication.RemoveMetodoPago(id);

            return Ok(response);
        }
    }
}
