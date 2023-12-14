using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Dtos.Direccion.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Infrastructure.Commons.Bases.Request;

namespace ShoeStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccionApplication _direccionApplication;

        public DireccionController(IDireccionApplication direccionApplication)
        {
            _direccionApplication = direccionApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListDirecciones([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _direccionApplication.ListDirecciones(filters);

            return Ok(response);
        }

        [HttpGet("Select/{userId:int}")]
        public async Task<IActionResult> ListSelectDireccionesByUser(int userId)
        {
            var response = await _direccionApplication.ListSelectDireccionByUser(userId);

            return Ok(response);
        }

        [HttpGet("Usuario/{userId:int}")]
        public async Task<IActionResult> ListDireccionesByUser([FromQuery] BaseFiltersRequest filters, int userId)
        {
            var response = await _direccionApplication.ListDireccionesByUser(filters, userId);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> DireccionById(int id)
        {
            var response = await _direccionApplication.DireccionById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterDireccion([FromBody] DireccionRequestDto requestDto)
        {
            var response = await _direccionApplication.RegisterDireccion(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditDireccion(int id, [FromBody] DireccionRequestDto requestDto)
        {
            var response = await _direccionApplication.EditDireccion(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveDireccion(int id)
        {
            var response = await _direccionApplication.RemoveDireccion(id);

            return Ok(response);
        }
    }
}
